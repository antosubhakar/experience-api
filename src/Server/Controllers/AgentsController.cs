using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Mvc.Filters;
using Doctrina.ExperienceApi.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/agents")]
    [Produces("application/json")]
    public class AgentsController : ApiControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetAgentProfile(
            [BindRequired, FromQuery] Agent agent,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = await _agentService.GetPerson(agent, cancellationToken);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}
