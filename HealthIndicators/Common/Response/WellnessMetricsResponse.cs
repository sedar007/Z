namespace Common.Response;

public class WellnessMetricsResponse {
    public int IdUser { get; init; }
    public int Steps { get; init; }
    public float SleepDuration { get; init; }
    public int HeartRate { get; init; }
    public float Distance { get; init; }
    
    public float Weight { get; init; }
    public float Height { get; init; }
   
    public string CategoryImc { get; init; } = null!;
    public float Bmi { get; set; }
    public DateTime Date { get; set; }
    
    
}

