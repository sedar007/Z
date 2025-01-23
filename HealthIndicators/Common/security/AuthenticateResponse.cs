using Common.DTO;

namespace Common.security {
    public class AuthenticateResponse {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(UserAuthDto user, string token) {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
    }
}