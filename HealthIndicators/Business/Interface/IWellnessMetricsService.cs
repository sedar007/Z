using Common.DAO;
using Common.DTO;
using Common.Request;
using Common.Response;

namespace Business.Interface;
public interface IWellnessMetricsService {
    Task<WellnessMetricsDTO> Create(WellnessMetricsCreationRequest request);
    Task<WellnessMetricsResponse?> GetWellnessMetricsById(int id, string unit = "km");
    Task<WellnessMetricsResponse?> GetWellnessMetricsTodayByUserId(int idUser, string unit = "km");
    
    
}