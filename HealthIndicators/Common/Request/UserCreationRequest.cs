namespace Common.Request;

public class UserCreationRequest
{
    public string Name { get; init; }
    public int Age { get; init; }
    public float Weight { get; set; }
    public string UnitWeight { get; init; }
    public float Height { get; init; } 
    
    public string Password { get; init; }
    
}