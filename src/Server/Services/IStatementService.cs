using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    /// <summary>
    /// Service for creating or retrieving stored statements.
    /// </summary>
    public interface IStatementService
    {
        /// <summary>
        /// Get voided statement from store
        /// </summary>
        Task<Statement> GetVoidedStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get statement from store.
        /// </summary>
        Task<Statement> GetStatement(Guid statementId, bool attachments, ResultFormat format, CancellationToken cancellationToken = default);

        /// <summary>
        /// Store a statement with given id
        /// </summary>
        Task PutStatement(Guid statementId, Statement statement, CancellationToken cancellationToken = default);

        /// <summary>
        /// Create statement(s) with attachment(s)
        /// </summary>
        /// <returns>Array of Statement id(s) (UUID) in the same order as the corresponding stored Statements.</returns>
        Task<ICollection<Guid>> PostStatements(StatementCollection statements, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get <see cref="StatementsResult"/> matching <paramref name="parameters"/>
        /// </summary>
        Task<StatementsResult> GetStatementsResult(StatementsQuery parameters, CancellationToken cancellationToken = default);
    }
}
