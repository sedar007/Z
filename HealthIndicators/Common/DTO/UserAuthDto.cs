namespace Common.DTO;

public class UserAuthDto {
    
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    
    public int IdUser { get; set; }
		
    //[JsonIgnore]
    public string Password { get; set; } = null!;
}