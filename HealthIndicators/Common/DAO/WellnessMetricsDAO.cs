using Common.DAO;

namespace Common.DTO;

public class WellnessMetricsDAO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserDAO User { get; set; }
    public int Steps { get; set; }
    public float SleepDuration { get; set; }
    public int HeartRate { get; set; }
}
