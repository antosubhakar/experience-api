using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    public interface IActivityStateService
    {
        Task<ICollection<ActivityStateDocument>> GetActivityStates(Iri activityId, Agent agent, Guid? registration, DateTime? since, CancellationToken cancellationToken);
        Task<ActivityStateDocument> GetActivityState(string stateId, Iri activityId, Agent agent, Guid? registration, CancellationToken cancellationToken);
        Task<ActivityStateDocument> PostSingleState(string stateId, Iri activityId, Agent agent, byte[] body, string contentType, Guid? registration, CancellationToken cancellationToken);
        Task DeleteActivityState(string stateId, Iri activityId, Agent agent, Guid? registration, CancellationToken cancellationToken);
        Task DeleteActivityStates(Iri activityId, Agent agent, Guid? registration, CancellationToken cancellationToken);
    }
}
