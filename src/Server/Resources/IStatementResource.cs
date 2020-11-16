using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// The basic communication mechanism of the Experience API.
    /// </summary>
    public interface IStatementResource
    {
        /// <summary>
        /// Get voided statement from store
        /// </summary>
        /// <param name="statementId"></param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<Statement> GetVoidedStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get statement from store.
        /// </summary>
        /// <param name="statementId">Id of the Statement to fetch</param>
        /// <param name="attachments"></param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<Statement> GetStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken = default);

        /// <summary>
        /// Store a statement with given id
        /// </summary>
        /// <param name="statementId">Id of the Statement to store</param>
        /// <param name="statement">Statement to store.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task PutStatement(Guid statementId, Statement statement, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create statement(s) with attachment(s)
        /// </summary>
        /// <param name="statements">Collection of statements to store.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>Array of Statement id(s) (UUID) in the same order as the corresponding stored Statements.</returns>
        Task<ICollection<Guid>> PostStatements(StatementCollection statements, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get <see cref="StatementsResult"/> matching <paramref name="parameters"/>
        /// </summary>
        /// <param name="parameters">Query parameters to limit result.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<StatementsResult> GetStatementsResult(StatementsQuery parameters, CancellationToken cancellationToken = default);
    }
}
