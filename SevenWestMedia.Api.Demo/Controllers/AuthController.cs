using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SevenWestMedia.Api.Demo.Services;

namespace SevenWestMedia.Api.Demo.Controllers
{
    /// <summary>
    /// Authenticates client and returns token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates client and returns token
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<string>> Get([FromHeader] string apiKey)
        {
            var token = await _authService.GetAuthToken(apiKey);
            return Ok(token);
        }
    }
}
