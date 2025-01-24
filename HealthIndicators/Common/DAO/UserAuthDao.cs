namespace Common.DAO;

public class UserAuthDao {
    
    public int Id { get; set; }
    public string Username { get; set; } 
    public int UserId { get; set; }
    public UserDAO User { get; set; }
    //[JsonIgnore]
    public string Password { get; set; } = null!;
}