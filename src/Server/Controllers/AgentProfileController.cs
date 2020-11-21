using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Data.Documents;
using Doctrina.ExperienceApi.Server.Extensions;
using Doctrina.ExperienceApi.Server.Mvc.Filters;
using Doctrina.ExperienceApi.Server.Resources;
using Doctrina.ExperienceApi.Data.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Doctrina.ExperienceApi.Server.Mvc.ActionResults;
using Doctrina.ExperienceApi.Server.Models;

namespace Doctrina.ExperienceApi.Server.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/agents/profile")]
    public class AgentProfileController : ApiControllerBase
    {
        private readonly IAgentProfileResource profileService;

        public AgentProfileController(IAgentProfileResource profileService)
        {
            this.profileService = profileService;
        }

        private async Task<ActionResult> GetAgentProfilesAsync(
            [BindRequired, FromQuery] Agent agent,
            [FromQuery] DateTimeOffset? since = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MultipleDocumentResult result = await profileService.GetAgentProfiles(agent, since, cancellationToken);

            if (result.IsEmpty)
            {
                return Ok(Array.Empty<Guid>());
            }

            Response.Headers.Add(HeaderNames.LastModified, result.LastModified?.ToString("o"));
            return Ok(result.Ids);
        }

        [HttpGet(Order = 2)]
        [HttpHead]
        public async Task<ActionResult> GetAgentProfile(
            [BindRequired] Agent agent,
            [FromQuery] string profileId,
            [FromQuery] DateTimeOffset? since = null,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(string.IsNullOrEmpty(profileId))
            {
                return await GetAgentProfilesAsync(agent, since, cancellationToken);
            }

            IDocument profile = await profileService.GetAgentProfile(agent, profileId, cancellationToken);

            if (profile == null)
            {
                return NotFound();
            }

            if (Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            return new DocumentResult(profile);
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

            IDocument profile = await profileService.GetAgentProfile(agent, profileId, cancellationToken);

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

            Response.Headers.Add(HeaderNames.ETag, profile.Tag.ToOpaqueQuotedString());
            Response.Headers.Add(HeaderNames.LastModified, profile.LastModified?.ToString("o"));

            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteProfileAsync(
            [BindRequired, FromQuery] string profileId,
            [BindRequired] Agent agent,
            CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IDocument profile = await profileService.GetAgentProfile(agent, profileId, cancellationToken);

            if (Request.TryConcurrencyCheck(profile?.Tag, profile?.LastModified, out int statusCode))
            {
                return StatusCode(statusCode);
            }

            if (profile == null)
            {
                return NotFound();
            }

            await profileService.DeleteAgentProfile(agent, profileId, cancellationToken);

            return NoContent();
        }
    }
}
