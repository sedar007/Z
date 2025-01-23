using Common.DAO;
using Common.DTO;
using Common.Request;

namespace Business.Interface;
public interface IWellnessMetricsService {
    Task<WellnessMetricsDTO> Create(WellnessMetricsCreationRequest request);
}