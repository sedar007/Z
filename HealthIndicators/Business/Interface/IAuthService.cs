using Common.DTO;
using Common.Request;
using Common.security;

namespace Business.Interface;

public interface IAuthService {
	Task<AuthenticateResponse?> Login(LoginRequest request);
	Task<UserAuthDto> Create(UserAuthCreationRequest request);
}

