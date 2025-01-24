using Common.DAO;

namespace Common.Request;
public class WellnessMetricsCreationRequest
{
    public int IdUser { get; init; }
    public int Steps { get; init; }
    public float SleepDuration { get; init; }
    public int HeartRate { get; init; }
    public DateTime Date { get; set; } = DateTime.Now;
}