using Common.DTO;

namespace Common.DAO;

public class UserDAO 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public float Weight { get; set; }
    public float Height { get; set; }
    public UserAuthDao Auth { get; set; }
    public ICollection<WellnessMetricsDAO> WellnessMetrics { get; set; } = null!;
}