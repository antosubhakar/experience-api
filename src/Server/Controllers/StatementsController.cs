using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Mvc.ActionResults;
using Doctrina.ExperienceApi.Server.Mvc.Filters;
using Doctrina.ExperienceApi.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Controllers
{
    /// <summary>
    /// The basic communication mechanism of the Experience API.
    /// </summary>
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/statements")]
    [Produces("application/json")]
    public class StatementsController : ApiControllerBase
    {
        private readonly IStatementService _statementService;

        public StatementsController(IStatementService statementService)
        {
            _statementService = statementService;
        }

        /// <summary>
        /// Get statements
        /// </summary>
        [HttpGet(Order = 3)]
        [HttpHead]
        [Produces("application/json", "multipart/mixed")]
        public async Task<IActionResult> GetStatements([FromQuery] StatementsQuery parameters, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            parameters.Format = parameters.Format ?? ResultFormat.Exact;
            if (!StringValues.IsNullOrEmpty(Request.Headers[HeaderNames.AcceptLanguage]))
            {
                parameters.Format = ResultFormat.Canonical;
            }

            ResultFormat format = parameters.Format.Value;
            bool attachments = parameters.Attachments.GetValueOrDefault();

            if (parameters.StatementId.HasValue || parameters.VoidedStatementId.HasValue)
            {
                Task<Statement> statementTask = null;
                if (parameters.StatementId.HasValue)
                {
                    Guid statementId = parameters.StatementId.Value;
                    statementTask = _statementService.GetStatement(statementId, attachments, format, cancellationToken);
                }
                else if (parameters.VoidedStatementId.HasValue)
                {
                    Guid voidedStatementId = parameters.VoidedStatementId.Value;
                    statementTask = _statementService.GetVoidedStatement(voidedStatementId, attachments, format, cancellationToken);
                }

                Statement statement = await statementTask;

                if (statement == null)
                {
                    return NotFound();
                }

                return new StatementActionResult(statement, format);
            }

            StatementsResult result =  await _statementService.GetStatementsResult(parameters);
            return new StatementsActionResult(result, format, attachments);
        }

        /// <summary>
        /// Stores a single Statement with attachment(s) with the given id.
        /// </summary>
        [HttpPut]
        [Produces("application/json")]
        public async Task<IActionResult> PutStatement([FromQuery] Guid statementId, Statement statement, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _statementService.PutStatement(statementId, statement, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Create statement(s) with attachment(s)
        /// </summary>
        /// <returns>Array of Statement id(s) (UUID) in the same order as the corresponding stored Statements.</returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<ICollection<Guid>>> PostStatements(StatementCollection statements, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<Guid> guids = await _statementService.PostStatements(statements, cancellationToken);

            return Ok(guids);
        }
    }
}
