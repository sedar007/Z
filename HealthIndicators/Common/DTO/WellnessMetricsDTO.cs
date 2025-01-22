using Common.DTO;

namespace Common.DAO;

public class WellnessMetricsDTO
{
    public int Id { get; set; }
    public UserDTO User { get; set; }
    public int Steps { get; set; }
    public float SleepDuration { get; set; }
    public int HeartRate { get; set; }
}
