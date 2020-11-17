using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// Generally, this is a scratch area for Learning Record Providers that do not have their own internal storage, or need to persist state across devices.
    /// </summary>
    public interface IStateResource
    {
        /// <summary>
        /// Fetches State ids of all state data for this context (Activity + Agent [ + registration if specified]). 
        /// If "since" parameter is specified, this is limited to entries that have been stored or updated since the specified timestamp (exclusive).
        /// </summary>
        Task<ICollection<IDocument>> GetActivityStates(Iri activityId, Agent agent, Guid? registration = null, DateTimeOffset? since = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Stores, changes, fetches, or deletes the document specified 
        /// by the given "stateId" that exists in the context of the 
        /// specified Activity, Agent, and registration (if specified).
        /// </summary>
        Task<IDocument> GetActivityState(string stateId, Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellationToken = default);
        Task<IDocument> PostSingleState(string stateId, Iri activityId, Agent agent, byte[] body, string contentType, Guid? registration = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes the document specificed by given <paramref name="stateId"/> that exist in the context 
        /// of the specified <paramref name="activityId"/>, <paramref name="agent"/>, <paramref name="registration"/> (if specified).
        /// </summary>
        Task DeleteActivityState(string stateId, Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes all state data for this context (Activity + Agent [+ registration if specified]).
        /// </summary>
        Task DeleteActivityStates(Iri activityId, Agent agent, Guid? registration = null, CancellationToken cancellationToken = default);
    }
}
