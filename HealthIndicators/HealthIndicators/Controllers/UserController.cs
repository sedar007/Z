using Business.Interface;
using Common.DTO;
using Common.Request;
using Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace HealthIndicators.Controllers;

[ApiController]
[Route("api/user/")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) {
        _service = service;
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(UserCreationRequest request) {
        try {
            var data = await _service.Create(request);
            return Created("create", data);
        } catch (InvalidDataException ex) {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("getUser/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO?>> GetUserById(int id) {
        var user = await _service.GetUserById(id);
        if (user == null) {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet("getUserByName/{name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDTO?>> GetUserByName(string name) {
        var user = await _service.GetUserByName(name);
        if (user == null) {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpGet("getUsers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers() {
        return Ok(await _service.GetUsers());
    }
    
    [HttpDelete("remove/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Remove(int id) {
        try
        {
            await _service.Remove(id);
            return NoContent();
        } catch (InvalidDataException) {
            return NotFound();
        }
    }
    
    [HttpGet("getLast7DaysSteps/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserLast7StepsResponse>> GetUserLast7DaysSteps(int id) {
        try {
            return Ok(await _service.GetLast7DaysSteps(id));
        } catch (InvalidDataException) {
            return NotFound();
        }    
    }
    
}