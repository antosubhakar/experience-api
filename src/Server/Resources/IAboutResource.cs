using Doctrina.ExperienceApi.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// Returns JSON Object containing information about this LRS, including the xAPI version supported.
    /// Primarily this resource exists to allow Clients that support multiple xAPI versions to decide which version to use when communicating with the LRS. 
    /// Extensions are included to allow other uses to emerge.
    /// </summary>
    public interface IAboutResource
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns><see cref="About"/> object containing basic metadata about this LRS.</returns>
        Task<About> GetAbout(CancellationToken cancellationToken = default);
    }
}
