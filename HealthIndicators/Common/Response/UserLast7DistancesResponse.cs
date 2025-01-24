namespace Common.Response;

public class UserLast7DistancesResponse {
    public List<Dictionary<string, object>> Distances { get; init; }
    public float TotalDistances { get; init; }
}