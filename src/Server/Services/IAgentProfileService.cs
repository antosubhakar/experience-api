using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    public interface IAgentProfileService
    {
        Task<AgentProfileDocument> GetAgentProfile(string profileId, Agent agent, System.Threading.CancellationToken cancellationToken = default);
        Task<ICollection<AgentProfileDocument>> GetAgentProfiles(Agent agent, DateTimeOffset? since, CancellationToken cancellationToken = default);
        Task<AgentProfileDocument> CreateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken);
        Task<AgentProfileDocument> UpdateAgentProfile(Agent agent, string profileId, byte[] content, string contentType, CancellationToken cancellationToken);
        Task DeleteAgentProfile(string profileId, Agent agent, CancellationToken cancellationToken);
    }
}
