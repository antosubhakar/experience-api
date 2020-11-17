using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Mvc.Filters;
using Doctrina.ExperienceApi.Server.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/activities")]
    public class ActivitiesController : ApiControllerBase
    {
        private readonly IActivityResource activityService;

        public ActivitiesController(IActivityResource activityService)
        {
            this.activityService = activityService;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetActivity([FromQuery]Iri activityId, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Activity activity = await activityService.GetActivity(activityId, cancellationToken);

            if (activity == null)
            {
                return Ok(new Activity());
            }

            ResultFormat format = ResultFormat.Exact;
            if (!StringValues.IsNullOrEmpty(Request.Headers[HeaderNames.AcceptLanguage]))
            {
                format = ResultFormat.Canonical;
            }

            return Ok(activity.ToJson(format));
        }
    }
}
