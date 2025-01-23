using Business.Interface;
using Common.Request;
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
}
