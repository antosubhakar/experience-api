using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// The Activity Profile Resource is much like the State Resource, allowing for arbitrary key / document pairs to be saved which are related to an Activity.
    /// </summary>
    public interface IActivityProfileResource
    {
        /// <summary>
        /// Get Profile document for an activity matching <paramref name="activityId"/>.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <param name="registration"></param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<IDocument> GetActivityProfile(Iri activityId, string profileId, Guid? registration, CancellationToken cancellationToken = default);

        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="since">If "since" parameter is specified, this is limited to entries that have been stored or updated since the specified Timestamp (exclusive).</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        Task<ICollection<IDocument>> GetActivityProfiles(Iri activityId, DateTimeOffset? since, CancellationToken cancellationToken = default);
        Task UpdateProfile(Iri activityId, string profileId, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken = default);
        Task<IDocument> CreateProfile(Iri activityId, string profileId, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken = default);
        Task DeleteActivityProfile(Iri activityId, string profileId, Guid? registration, CancellationToken cancellationToken = default);
    }
}