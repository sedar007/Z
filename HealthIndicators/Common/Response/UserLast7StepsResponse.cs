namespace Common.Response;

public class UserLast7StepsResponse {
    public List<Dictionary<string, object>> Steps { get; init; }
    public int TotalSteps { get; init; }
    
}