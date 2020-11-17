using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// The Agent Profile Resource is much like the State Resource, allowing for arbitrary key / document pairs to be saved which are related to an Agent.
    /// </summary>
    public interface IAgentProfileResource
    {
        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Agent. 
        /// </summary>
        /// <param name="agent">The agent limit by.</param>
        /// <param name="profileId">The id associated with the <see cref="AgentProfileDocument"/>.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<IDocument> GetAgentProfile(Agent agent, string profileId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Agent. 
        /// </summary>
        /// <param name="agent">The agent limit by.</param>
        /// <param name="since">If specified, this limits to entries that have been stored or updated since the specified Timestamp (exclusive)</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<ICollection<IDocument>> GetAgentProfiles(Agent agent, DateTimeOffset? since = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Stores an document profile in the context of the specified <paramref name="agent"/>.
        /// </summary>
        /// <param name="agent">The <see cref="Agent"/> associated with this Profile document.</param>
        /// <param name="profileId">The id associated with the <see cref="AgentProfileDocument"/>.</param>
        /// <param name="content">The contents of the <see cref="AgentProfileDocument"/></param>
        /// <param name="contentType">The content type of the <see cref="AgentProfileDocument"/></param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<IDocument> CreateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update an <see cref="AgentProfileDocument"/> in the store.
        /// </summary>
        /// <param name="agent">The agent associated with the profile.</param>
        /// <param name="profileId">The profile id associated with the profile.</param>
        /// <param name="content">The contents of the profile.</param>
        /// <param name="contentType">The content type of the profile.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<IDocument> UpdateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deltes an <see cref="AgentProfileDocument"/> from the store.
        /// </summary>
        /// <param name="agent">The agent associated with the profile.</param>
        /// <param name="profileId">The profile id associated with the profile.</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task DeleteAgentProfile(Agent agent, string profileId, CancellationToken cancellationToken = default);
    }
}
