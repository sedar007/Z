using System.ComponentModel.DataAnnotations;
namespace Common.Request;

public class LoginRequest {

    [Required] 
    public string Username { get; set; } = null!;
    
    [Required]
    public string Password { get; set; } = null!;
}
