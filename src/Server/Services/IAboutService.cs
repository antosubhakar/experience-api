using Doctrina.ExperienceApi.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    /// <summary>
    /// The about service relies on it's services to populate <see cref="About"/> response.
    /// </summary>
    public interface IAboutService
    {
        Task<About> GetAbout(CancellationToken cancellationToken = default);
    }
}
