using Doctrina.ExperienceApi.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Resources
{
    /// <summary>
    /// The Activities Resource provides a method to retrieve a full description of an Activity from the LRS. <br />
    /// This resource has Concurrency controls associated with it.
    /// </summary>
    public interface IActivityResource
    {
        /// <summary>
        /// Loads the complete Activity Object specified.
        /// </summary>
        /// <param name="activityId">The id associated with the Activities to load.	</param>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>If no match was found, an <see cref="Activity"/> Object is still returned.</returns>
        Task<Activity> GetActivity(Iri activityId, CancellationToken cancellationToken = default);
    }
}
