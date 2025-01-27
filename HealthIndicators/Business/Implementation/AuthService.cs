﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Interface;
using Common.DTO;
using Common.DTO.Helper;
using Common.Request;
using Common.security;
using DataAccess.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Business.Implementation; 
public class AuthService : IAuthService {
	private readonly IAuthDataAccess _authDataAccess;
	private readonly ILogger<AuthService> _logger;
	private readonly IConfiguration _configuration;
	
	public AuthService(ILogger<AuthService> logger, IAuthDataAccess authDataAccess, IConfiguration configuration) {
		_logger = logger;
		_authDataAccess = authDataAccess;
		_configuration = configuration;
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
			
			
			return new AuthenticateResponse(user, GenerateJwtToken(request.Username, user.Id));
		}
		catch (Exception e) {
			_logger.LogError(e, e.Message);
			throw;
		}
	}
	
	private string GenerateJwtToken(string username, int userId) {
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
			new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
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
	
	
	public async Task<UserAuthDto> Create(string username, string password, int userId)  {
		try
		{
			
			var hashedPassword = HashPassword(password);
			if (string.IsNullOrEmpty(hashedPassword))
				throw new Exception("Password could not be hashed");
			
			return (await _authDataAccess.Create(username, hashedPassword,  userId)).ToDto();
		} catch (Exception e) {
			_logger.LogError(e, e.Message);
			throw;
		}
	}
	
	public async Task<UserAuthDto?> GetUserById(int id) {
		try {
			return (await _authDataAccess.GetUserById(id))?.ToDto();
		} catch (Exception e) {
			_logger.LogError(e, e.Message);
			throw;
		}
	}
	
}

