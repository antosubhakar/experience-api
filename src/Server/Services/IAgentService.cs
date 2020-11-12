using Doctrina.ExperienceApi.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    public interface IAgentService
    {
        Task<Person> GetPerson(Agent agent, CancellationToken cancellationToken);
    }
}
