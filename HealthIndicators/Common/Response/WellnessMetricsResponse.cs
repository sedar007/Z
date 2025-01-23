namespace Common.Response;

public class WellnessMetricsResponse {
    public int IdUser { get; init; }
    public int Steps { get; init; }
    public float SleepDuration { get; init; }
    public int HeartRate { get; init; }
    public float Distance { get; init; }
    public DateTime Date { get; set; } = DateTime.Now;
}