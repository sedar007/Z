using Common.DTO;
using Common.Request;

namespace DataAccess.Interface;
public interface IWellnessMetricsDataAccess
{
    Task<WellnessMetricsDAO?> GetWellnessMetricsById(int id);
    Task<WellnessMetricsDAO?> GetWellnessMetricsTodayByUserId(int idUser);
    Task<WellnessMetricsDAO> Create(WellnessMetricsCreationRequest request);
}
