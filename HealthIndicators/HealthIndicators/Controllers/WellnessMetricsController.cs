using Microsoft.AspNetCore.Mvc;
using DataAccess;

[ApiController]
[Route("api/[controller]")]
public class WellnessMetricsController : ControllerBase
{
    private readonly HealthContext _context;

    public WellnessMetricsController(HealthContext context)
    {
        _context = context;
    }

    // GET: /api/WellnessMetrics/
    [HttpGet]
    public IActionResult GetAllWellnessMetrics()
    {
        var metrics = _context.WellnessMetrics.ToList();
        return Ok(metrics);
    }

    // GET: /api/WellnessMetrics/{id}
    [HttpGet("{id}")]
    public IActionResult GetWellnessMetricById(int id)
    {
        var metric = _context.WellnessMetrics.Find(id);
        if (metric == null)
        {
            return NotFound();
        }

        return Ok(metric);
    }
}