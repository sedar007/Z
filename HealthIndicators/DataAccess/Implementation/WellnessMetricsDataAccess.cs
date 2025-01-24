using Common.DTO;
using Common.Request;
using Common.Response;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation;

public class WellnessMetricsDataAcess : IWellnessMetricsDataAccess
{ 
    private readonly HealthContext _context;
    private readonly IUserDataAccess _userDataAccess;
    
    public WellnessMetricsDataAcess(HealthContext context, IUserDataAccess userDataAccess) {
        _context = context;
        _userDataAccess = userDataAccess;
    }
    public Task<WellnessMetricsDAO?> GetWellnessMetricsById(int id) {
        return _context.WellnessMetrics.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<WellnessMetricsDAO> Create(WellnessMetricsCreationRequest request)
    {
        var newData = _context.WellnessMetrics.Add(new WellnessMetricsDAO {
            UserId = request.IdUser,
            Steps = request.Steps,
            SleepDuration = request.SleepDuration,
            HeartRate = request.HeartRate,
            Date = DateTime.UtcNow
           
        });
        await _context.SaveChangesAsync();
        return await GetWellnessMetricsById(newData.Entity.Id) ?? throw new NullReferenceException("Erreur lors de la creation des données de santé");
    }
    
    public async Task<UserLast7StepsResponse> GetUserLast7DaysSteps(int userId) {
        var last7DaysData = await _context.WellnessMetrics
            .Where(metric => metric.UserId == userId && metric.Date >= DateTime.UtcNow.AddDays(-7))
            .ToListAsync();
        
        var maxStepsPerDay = last7DaysData
            .GroupBy(metric => metric.Date.Date) 
            .Select(group => new {
                Date = group.Key.ToString("yyyy-MM-dd"),
                MaxSteps = group.Max(metric => metric.Steps) 
            }).ToList();

        var response = new UserLast7StepsResponse {
            Steps = maxStepsPerDay.Select(entry => new Dictionary<string, object> {
                { "date", entry.Date },
                { "steps", entry.MaxSteps }
            }).ToList(),
            TotalSteps = maxStepsPerDay.Sum(entry => entry.MaxSteps) 
        };

        return response;
    }


}