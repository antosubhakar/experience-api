using Doctrina.ExperienceApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Services
{
    public interface IActivityService
    {
        Task<Activity> GetActivity(Iri activityId, CancellationToken cancellationToken);
    }
}
