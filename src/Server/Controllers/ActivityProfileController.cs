using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Extensions;
using Doctrina.ExperienceApi.Server.Mvc.Filters;
using Doctrina.ExperienceApi.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/activities/profile")]
    [Produces("application/json")]
    public class ActivityProfileController : ApiControllerBase
    {
        private readonly IActivityProfileService _profileService;

        public ActivityProfileController(IActivityProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <returns>200 OK, the Profile document</returns>
        [HttpGet(Order = 1)]
        [HttpHead(Order = 1)]
        public async Task<IActionResult> GetProfile(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Iri activityId,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ActivityProfileDocument profile = await _profileService.GetActivityProfile(profileId, activityId, registration, cancellationToken);

            if (profile == null)
            {
                return NotFound();
            }

            var result = new FileContentResult(profile.Content, profile.ContentType)
            {
                EntityTag = new EntityTagHeaderValue($"\"{profile.Tag}\""),
                LastModified = profile.LastModified
            };

            return result;
        }

        /// <summary>
        /// Fetches Profile ids of all Profile documents for an Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with these Profile documents.</param>
        /// <param name="since">Only ids of Profile documents stored since the specified Timestamp (exclusive) are returned.</param>
        /// <returns>200 OK, Array of Profile id(s)</returns>
        [HttpGet(Order = 2)]
        [HttpHead(Order = 2)]
        public async Task<ActionResult<string[]>> GetProfiles(
            [BindRequired, FromQuery] Iri activityId,
            [FromQuery] DateTimeOffset? since = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ICollection<ActivityProfileDocument> profiles = await _profileService.GetActivityProfiles(activityId, since, cancellationToken);
                
            if (profiles == null)
            {
                return Ok(Array.Empty<string>());
            }

            IEnumerable<string> ids = profiles.Select(x => x.ProfileId);
            string lastModified = profiles.OrderByDescending(x => x.LastModified)
                .FirstOrDefault()?.LastModified?.ToString("o");

            Response.Headers.Add("Last-Modified", lastModified);
            return Ok(ids);
        }

        /// <summary>
        /// Stores or changes the specified Profile document in the context of the specified Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <param name="document">The document to be stored or updated.</param>
        /// <returns>204 No Content</returns>
        [HttpPost]
        [HttpPut]
        public async Task<IActionResult> SaveProfile(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Iri activityId,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            [BindRequired, FromBody] byte[] body,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ActivityProfileDocument profile = await _profileService.GetActivityProfile(profileId, activityId, registration, cancellationToken);

            if (Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if (profile != null)
            {
                // Optimistic Concurrency
                if (HttpMethods.IsPut(Request.Method))
                {
                    return Conflict();
                }

                await _profileService.UpdateProfile(profileId, activityId, body, contentType, registration, cancellationToken);
            }
            else
            {
               profile = await _profileService.CreateProfile(profileId, activityId, body, contentType, registration, cancellationToken);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the specified Profile document in the context of the specified Activity.
        /// </summary>
        /// <param name="activityId">The Activity id associated with this Profile document.</param>
        /// <param name="profileId">The profile id associated with this Profile document.</param>
        /// <returns>204 No Content</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Iri activityId,
            [FromQuery] Guid? registration = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await _profileService.GetActivityProfile(profileId, activityId, registration, cancellationToken);

            if (profile == null)
            {
                return NotFound();
            }

            if (Request.TryConcurrencyCheck(profile.Tag, profile.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            await _profileService.DeleteActivityProfile(profileId, activityId, registration, cancellationToken);

            return NoContent();
        }
    }
}
