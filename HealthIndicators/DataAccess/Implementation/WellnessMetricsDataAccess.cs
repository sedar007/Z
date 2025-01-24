using Common.DTO;
using Common.Request;
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
    
    public Task<WellnessMetricsDAO?> GetWellnessMetricsTodayByUserId(int idUser) {
        return _context.WellnessMetrics.FirstOrDefaultAsync(x => x.UserId == idUser);
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
}