using Common.DAO;
using Common.DTO;
using Common.Request;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interface;

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
            HeartRate = request.HeartRate
        });
        await _context.SaveChangesAsync();
        return await GetWellnessMetricsById(newData.Entity.Id) ?? throw new NullReferenceException("Erreur lors de la creation des données de santé");
    }
}