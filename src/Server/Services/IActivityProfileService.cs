using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    public interface IActivityProfileService
    {
        Task<ActivityProfileDocument> GetActivityProfile(string profileId, Iri activityId, Guid? registration, CancellationToken cancellationToken);
        Task<ICollection<ActivityProfileDocument>> GetActivityProfiles(Iri activityId, DateTimeOffset? since, CancellationToken cancellationToken);
        Task UpdateProfile(string profileId, Iri activityId, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken);
        Task<ActivityProfileDocument> CreateProfile(string profileId, Iri activityId, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken);
        Task DeleteActivityProfile(string profileId, Iri activityId, Guid? registration, CancellationToken cancellationToken);
    }
}