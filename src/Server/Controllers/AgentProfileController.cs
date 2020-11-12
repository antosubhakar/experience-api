using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Mvc.Filters;
using Doctrina.ExperienceApi.Server.Services;
using Microsoft.AspNetCore.Authorization;
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
    [Route("xapi/agents/profile")]
    public class AgentProfileController : ApiControllerBase
    {
        private readonly IAgentProfileService profileService;

        public AgentProfileController(IAgentProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpGet(Order = 1)]
        [HttpHead]
        public async Task<ActionResult> GetAgentProfile(
            [BindRequired, FromQuery] string profileId,
            [BindRequired] Agent agent,
            CancellationToken cancellationToken = default)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgentProfileDocument profile = await profileService.GetAgentProfile(profileId, agent, cancellationToken);

            if (profile == null)
            {
                return NotFound();
            }

            if (Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            var result = new FileContentResult(profile.Content, profile.ContentType)
            {
                LastModified = profile.LastModified
            };

            Response.Headers.Add(HeaderNames.ETag, $"\"{profile.Tag}\"");
            return result;
        }

        [HttpGet(Order = 2)]
        public async Task<ActionResult> GetAgentProfilesAsync(
            [BindRequired, FromQuery] Agent agent,
            [FromQuery] DateTimeOffset? since = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profiles = await profileService.GetAgentProfiles(agent, since, cancellationToken);

            if (profiles == null || profiles.Count == 0)
            {
                return Ok(Array.Empty<Guid>());
            }

            IEnumerable<string> ids = profiles.Select(x => x.ProfileId).ToList();

            string lastModified = profiles.OrderByDescending(x => x.LastModified)
                .FirstOrDefault()?.LastModified?.ToString("o");
            Response.Headers.Add(HeaderNames.LastModified, lastModified);
            return Ok(ids);
        }

        [HttpPut]
        [HttpPost]
        public async Task<ActionResult> SaveAgentProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired, FromQuery] Agent agent,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            [BindRequired, FromBody] byte[] content,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AgentProfileDocument profile = await profileService.GetAgentProfile(profileId, agent, cancellationToken);

            if (Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if (profile == null)
            {
                profile = await profileService.CreateAgentProfile(agent, profileId, content, contentType, cancellationToken);
            }
            else
            {
                profile = await profileService.UpdateAgentProfile(agent, profileId, content, contentType, cancellationToken);
            }

            Response.Headers.Add(HeaderNames.ETag, $"\"{profile.Tag}\"");
            Response.Headers.Add(HeaderNames.LastModified, profile.LastModified?.ToString("o"));

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired] Agent agent,
            [BindRequired, FromHeader(Name = "Content-Type")] string contentType,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var profile = await profileService.GetAgentProfile(profileId, agent, cancellationToken);

            if (Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if (profile == null)
            {
                return NotFound();
            }

            await profileService.DeleteAgentProfile(profileId, agent, cancellationToken);

            return NoContent();
        }
    }
}
