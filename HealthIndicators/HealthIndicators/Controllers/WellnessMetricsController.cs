using Business.Interface;
using Common.Request;
using Common.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthIndicators.Controllers;

[ApiController]
[Route("api/metrics/")]
public class WellnessMetricsController : ControllerBase
{ 
    private readonly IWellnessMetricsService _service;

    public WellnessMetricsController(IWellnessMetricsService service) {
        _service = service;
    }

    /// <summary>
    /// Creates a new wellness metric entry based on the provided data.
    /// </summary>
    /// <param name="request">The request body containing wellness metrics data.</param>
    /// <returns>Returns the created wellness metrics data along with a 201 status code.</returns>
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(WellnessMetricsCreationRequest request) {
        try {
            var data = await _service.Create(request);
            return Created($"/api/metrics/{data.Id}", data);               
        } catch (InvalidDataException ex) {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Retrieves wellness metrics for a specific ID.
    /// </summary>
    /// <param name="id">The wellness metric ID to retrieve.</param>
    /// <param name="unit">The unit of measurement for distance (miles or km).</param>
    /// <returns>Returns the wellness metrics for the given ID in the specified unit.</returns>
    [HttpGet("getMetric/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WellnessMetricsResponse?>> GetWellnessMetricsById(int id, [FromQuery] string? unit = "km")
    {
        try
        {
            var metric = await _service.GetWellnessMetricsById(id, unit ?? "km");
            if (metric == null)
            {
                return NotFound();
            }
            return Ok(metric);
        }
        catch (InvalidDataException e)
        {
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Retrieves the wellness metrics for today for a given user by user ID.
    /// </summary>
    /// <param name="idUser">The ID of the user to retrieve wellness metrics for.</param>
    /// <param name="unit">The unit of measurement for distance (miles or km).</param>
    /// <returns>Returns the wellness metrics for today for the specified user.</returns>
    [HttpGet("getMetric/today/byAuthId/{idAuth}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<WellnessMetricsResponse?>> GetWellnessMetricsTodayByUserId(int idAuth, [FromQuery] string? unit = "km")
    {
        try
        {
            var metric = await _service.GetWellnessMetricsTodayByUserId(idAuth, unit ?? "km");
            if (metric == null) {
                return NotFound();
            }
            return Ok(metric);
        }
        catch (UnauthorizedAccessException e)
        {
            return  StatusCode(StatusCodes.Status403Forbidden);
        }
        catch (InvalidDataException e)
        {
            return BadRequest(e.Message);
        }
    }
}
