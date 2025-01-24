using Microsoft.AspNetCore.Mvc;
namespace HealthIndicators.Controllers;

[ApiController]
[Route("")]
public class HealthController : ControllerBase {
    
    /// <summary>
    /// Checks the health status of the API.
    /// </summary>
    /// <returns>A message indicating that the API is online.</returns>
    [HttpGet("health")]
    public IActionResult GetHealth() {
        return Ok("API is alive");
    }
}
