using Common.DTO;
using Common.Request;
using Common.Response;

namespace DataAccess.Interface;
public interface IWellnessMetricsDataAccess
{
    Task<WellnessMetricsDAO?> GetWellnessMetricsById(int id);
    
    Task<WellnessMetricsDAO> Create(WellnessMetricsCreationRequest request);
    Task<UserLast7StepsResponse> GetUserLast7DaysSteps(int userId);
}
