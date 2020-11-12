using Doctrina.ExperienceApi.Data;
using Doctrina.ExperienceApi.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.ExperienceApi.Server.Controllers
{
    [Route("xapi/about")]
    [ApiController]
    [Produces("application/json")]
    public class AboutController : ApiControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<About>> About(CancellationToken cancellationToken = default)
        {
            return Ok(await _aboutService.GetAbout(cancellationToken));
        }
    }
}
