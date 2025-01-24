using Microsoft.AspNetCore.Mvc;
using Business.Interface;
using Common.Request;

namespace HealthIndicators.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">The login request.</param>
        /// <returns>An action result.</returns>
        /// <response code="200">Returns the logged-in user information.</response>
        /// <response code="401">If the user is unauthorized.</response>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Loging([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _authService.Login(request);

                if (response == null)
                    throw new Exception("something went wrong");

                _logger.LogInformation("User logged in");
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogWarning("User not unauthorized");
                return Unauthorized();
            }
        }
    }
}