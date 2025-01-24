using Common.DTO;
using Common.Request;
using Common.security;

namespace Business.Interface;

public interface IAuthService {
	Task<AuthenticateResponse?> Login(LoginRequest request);
	Task<UserAuthDto> Create(string username, string password, int userId);
	Task<UserAuthDto?> GetUserById(int id);
}

