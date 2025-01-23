using Business.Interface;
using Common.DTO;
using Common.Request;
using Common.security;
using Microsoft.AspNetCore.Mvc;
namespace HealthIndicators.Controllers.Admin;

	
[ApiController]
[Route("api/admin/[controller]")]
public class AuthController : ControllerBase{
	
	private readonly IAuthService _authService;
	private readonly ILogger<AuthController> _logger;
	
	public AuthController(IAuthService authService, ILogger<AuthController> logger){
		_authService = authService;
		_logger = logger;
	}
	
	
	[HttpPost("login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	 public async Task<IActionResult> Loging([FromBody] LoginRequest request) {
		try {
			var response = await _authService.Login(request);
			
			if (response == null) 
				throw new Exception("something went wrong");
			
			_logger.LogInformation("User logged in");
			return Ok(response);
		} 
		catch (Exception e) {
			_logger.LogWarning("User not unauthorized");
			return Unauthorized();
		}
	}
	 
	 
	[HttpPost]
	[Route("create")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<ActionResult> Create(UserAuthCreationRequest request) {
		try {
			var data = await _authService.Create(request);
			return Ok(data);
		} catch (InvalidDataException ex) {
			return BadRequest(ex.Message);
		}
	}

}


