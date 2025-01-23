namespace Common.Request;
using Common.DAO;
public class UserAuthCreationRequest {
    
    public string Username { get; init; }
    public string Password { get; set; }
    
    //[JsonIgnore]
    public int UserId { get; set; }
}

