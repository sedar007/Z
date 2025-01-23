using Business.Interface;
using Common.Request;
using Common.Response;
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

}
