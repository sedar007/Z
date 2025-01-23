using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Common.Request;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;
using Business.Interface;
using Common.security;
using Microsoft.AspNetCore.Identity;
using Common.DTO.Helper;
using Common.DTO;

namespace Business.Implementation; 
public class AuthService : IAuthService {
	private readonly IAuthDataAccess _authDataAccess;
	private readonly ILogger<AuthService> _logger;
	private readonly IConfiguration _configuration;
	private readonly IUserService _userService;
	
	public AuthService(ILogger<AuthService> logger, IAuthDataAccess authDataAccess, IConfiguration configuration,
						IUserService userService) {
		_logger = logger;
		_authDataAccess = authDataAccess;
		_configuration = configuration;
		_userService = userService;
	}
	
	public async Task<AuthenticateResponse?> Login(LoginRequest request) {
		if(request == null)
			throw new ArgumentNullException(nameof(request));
		
		try {
			var user = (await _authDataAccess.GetUser(request.Username))?.ToDto();
			if (user == null) {
				_logger.LogWarning("User not Found");
				throw new Exception(request.Username + " User not Found");
			}
			
			if(! VerifyPassword(user.Password, request.Password)) {
				_logger.LogWarning("Invalid Password");
				throw new Exception("Invalid Password");
			}
			return new AuthenticateResponse(user, GenerateJwtToken(request.Username));
		}
		catch (Exception e) {
			_logger.LogError(e, e.Message);
			throw;
		}
	}
	
	private string GenerateJwtToken(string username) {
		//var jwtSettings = _configuration.GetSection("Jwt").Get<JwtSettings>();
		
		var jwtSettings = new JwtSettings {
			Key = _configuration.GetSection("Key")?.Get<string>() ?? string.Empty,
			Issuer = _configuration.GetSection("Issuer")?.Value ?? string.Empty,
			Audience = _configuration.GetSection("Audience")?.Get<string>() ?? string.Empty,
			ExpireMinutes = _configuration.GetSection("ExpireMinutes")?.Get<int>() ?? 0,
		};
		
		if(jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
			throw new Exception("Jwt settings are not configured properly");
		
		var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Key));
		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var claims = new[] {
			new Claim(JwtRegisteredClaimNames.Sub, username),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};
		
		var token = new JwtSecurityToken(
			issuer: jwtSettings.Issuer,
			audience: jwtSettings.Audience,
			claims: claims,
			expires: DateTime.Now.AddMinutes(jwtSettings.ExpireMinutes),
			signingCredentials: creds
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
	
	private string HashPassword(string providedPassword) {
		if(string.IsNullOrEmpty(providedPassword)) return "";
		PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
		return hasher.HashPassword(new IdentityUser(), providedPassword);
	}
	
	private bool VerifyPassword(string hashedPassword, string providedPassword) {
		PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
		return hasher.VerifyHashedPassword( new IdentityUser(), 
			hashedPassword, providedPassword) == PasswordVerificationResult.Success;
	}
	
	
	public async Task<UserAuthDto> Create(UserAuthCreationRequest request) {
		try
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			var user = await _authDataAccess.GetUser(request.Username);
			if (user != null)
				throw new InvalidDataException("User already exists");

			var hashedPassword = HashPassword(request.Password);
			if (string.IsNullOrEmpty(hashedPassword))
				throw new Exception("Password could not be hashed");

			/*var userCreationRequest = new UserCreationRequest {
				Name = request.Username,
				Age = 0,
				Weight = 0,
				Height = 0,
				Password = hashedPassword
			};*/

			var userS = await _userService.Create(new UserCreationRequest { 
				Name = request.Username,
				Age = 0,
				Weight = 0,
				Height = 0
			});

			request.UserId = userS.Id;
			request.Password = hashedPassword;
			
			return (await _authDataAccess.Create(request)).ToDto();
		} catch (Exception e) {
			_logger.LogError(e, e.Message);
			throw;
		}
	}
	
}

