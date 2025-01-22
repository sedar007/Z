using Microsoft.AspNetCore.Mvc;
namespace HealthIndicators.Controllers;

[ApiController]
[Route("")]
public class HealthController : ControllerBase {
        
    [HttpGet("health")]
    public IActionResult GetHealth() {
        return Ok("API is alive");
    }
}
