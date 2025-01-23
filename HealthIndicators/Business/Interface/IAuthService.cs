using Common.DTO;
using Common.security;
using Common.Request;

namespace Business.Interface;

public interface IAuthService {
	Task<AuthenticateResponse?> Login(LoginRequest request);
	Task<UserAuthDto> Create(string username, string password, int userId);
}

