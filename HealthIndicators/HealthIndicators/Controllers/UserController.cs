using Business.Interface;
using Common.DTO;
using Common.Request;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using Business.Interface;
using Business.Tools;
using Common.DAO;
using Common.DTO;
using Common.DTO.Helper;
using Common.Request;
using Common.Response;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace HealthIndicators.Controllers;

[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;

    public UserController(IUserService service, IHttpContextAccessor httpContextAccessor, IAuthService authService)
    {
        _service = service;
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }
   

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">The user creation request containing necessary information.</param>
    /// <returns>The created user object.</returns>
    /// <response code="201">User created successfully.</response>
    /// <response code="400">Invalid request data.</response>
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(UserCreationRequest request)
    {
        try
        {
            var data = await _service.Create(request);
            return Created("create", data);
        }
        catch (InvalidDataException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns>The user object.</returns>
    /// <response code="200">User found and returned.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("getUser/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO?>> GetUserById(int id)
    {
        var user = await _service.GetUserById(id);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    /// <summary>
    /// Retrieves a user by their name.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    /// <returns>The user object.</returns>
    /// <response code="200">User found and returned.</response>
    /// <response code="404">User not found.</response>
    [HttpGet("getUserByName/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO?>> GetUserByName(string name)
    {
        var user = await _service.GetUserByName(name);
        if (user == null)
        {
            return NotFound("User not found");
        }
        return Ok(user);
    }

    /// <summary>
    /// Retrieves a list of all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    /// <response code="200">List of users retrieved successfully.</response>
    [HttpGet("getUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
    {
        return Ok(await _service.GetUsers());
    }

    /// <summary>
    /// Removes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to remove.</param>
    /// <returns>No content if successful.</returns>
    /// <response code="204">User removed successfully.</response>
    /// <response code="404">User not found.</response>
    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Remove(int id)
    {
        try
        {
            await _service.Remove(id);
            return NoContent();
        }
        catch (InvalidDataException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves the steps data for the last 7 days for a user.
    /// </summary>
    /// <param name="idAuth">The ID of the user.</param>
    /// <returns>The steps data for the last 7 days.</returns>
    /// <response code="200">Steps data retrieved successfully.</response>
    /// <response code="404">User not found.</response>
    /// <response code="403">Access denied.</response>
    [HttpGet("getLast7DaysSteps/{idAuth}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserLast7StepsResponse>> GetUserLast7DaysSteps(int idAuth)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
            if (userId == null || userId != idAuth.ToString()) throw new UnauthorizedAccessException("Access denied");

            var userAuth = await _authService.GetUserById(idAuth);
            
            var user = await _service.GetUserById(userAuth.IdUser);
            if (user == null) return NotFound("User not found");
            
            return Ok(await _service.GetLast7DaysSteps(user.Id));
        }
        catch (UnauthorizedAccessException e)
        {
            return  StatusCode(StatusCodes.Status403Forbidden);
        }
       
        catch (InvalidDataException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Retrieves the distance data for the last 7 days for a user.
    /// </summary>
    /// <param name="idAuth">The ID of the user.</param>
    /// <returns>The distance data for the last 7 days.</returns>
    /// <response code="200">Distance data retrieved successfully.</response>
    /// <response code="404">User not found.</response>
    /// <response code="403">Access denied.</response>
    [HttpGet("getLast7DaysDistances/{idAuth}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserLast7StepsResponse>> GetUserLast7DaysDistances(int idAuth)
    {
        try
        {
            
            
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
            if (userId == null || userId != idAuth.ToString()) throw new UnauthorizedAccessException("Access denied");

            var userAuth = await _authService.GetUserById(idAuth);
            
            var user = await _service.GetUserById(userAuth.IdUser);
            if (user == null) return NotFound("User not found");
            return Ok(await _service.GetLast7DaysDistances(user.Id));
        }
        catch (UnauthorizedAccessException e)
        {
            return  StatusCode(StatusCodes.Status403Forbidden);
        }
       
        catch (InvalidDataException)
        {
            return NotFound();
        }
    }
}
