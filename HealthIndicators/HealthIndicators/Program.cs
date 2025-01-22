using Common;
using Common.security;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace HealthIndicators;

public class Program {
	public static void Main(string[] args) {

		var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
		logger.Info("Starting Application ...");

		try {
			var builder = WebApplication.CreateBuilder(args);
			builder.Configuration.AddUserSecrets<Program>();
			var rawConfig = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddEnvironmentVariables()
				.AddJsonFile("appsettings.json")
				.AddUserSecrets<Program>()
				.Build();

			var appSettingsSection = rawConfig.GetSection("AppSettings");
			builder.Services.Configure<AppSettings>(appSettingsSection);
			builder.Services.Configure<AppSettings>(builder.Configuration);


			// Context
			builder.Services.AddTransient<HealthContext>();

			// Add services to the container.
			

			
			
			builder.Services.AddCors(options => {
				options.AddPolicy("AllowAllOrigins",
					builder => {
						builder.AllowAnyOrigin()
							.WithMethods("GET")
							.AllowAnyHeader();
					});
			});
			
			
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Health", Version = "v1.0.0" });
				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Please enter token",
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					BearerFormat = "JWT",
					Scheme = "bearer"
				});
				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						new string[]{}
					}
				});
			});
        

			// NLog: Setup NLog for Dependency injection
			builder.Logging.ClearProviders();
			builder.Host.UseNLog();
			
			//var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
		/*	var jwtSettings = new JwtSettings {
				Key = builder.Configuration.GetSection("Key")?.Get<string>() ?? string.Empty,
				Issuer = builder.Configuration.GetSection("Issuer")?.Value ?? string.Empty,
				Audience = builder.Configuration.GetSection("Audience")?.Get<string>() ?? string.Empty,
				ExpireMinutes = builder.Configuration.GetSection("ExpireMinutes")?.Get<int>() ?? 0,
			};
			if (jwtSettings != null){
				var key = Encoding.ASCII.GetBytes((string) jwtSettings.Key);
				builder.Services.AddAuthentication(options =>
					{
						options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
						options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
					})
					.AddJwtBearer(options =>
					{
						options.TokenValidationParameters = new TokenValidationParameters
						{
							ValidateIssuer = true,
							ValidateAudience = true,
							ValidateLifetime = true,
							ValidateIssuerSigningKey = true,
							ValidIssuer = jwtSettings.Issuer,
							ValidAudience = jwtSettings.Audience,
							IssuerSigningKey = new SymmetricSecurityKey(key)
						};
					});
			}
			else 
				logger.Warn("JwtSettings not found in configuration");*/
			
			
			// Configuration pour le service keep-alive
			
			var app = builder.Build();

			using (var scope = app.Services.CreateScope()) {
				var dbContext = scope.ServiceProvider.GetRequiredService<HealthContext>();

				// Here is the migration executed
				dbContext.Database.Migrate();
			}

			// Configure the HTTP request pipeline.
			
				app.UseSwagger();
				app.UseSwaggerUI();
		
			
			// To remode in production
			/*if(app.Environment.IsProduction()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}*/

			app.UseHttpsRedirection();
			app.UseCors("AllowAllOrigins");
			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		} catch (Exception e) {
			logger.Error(e);
			throw;
		} finally {
			// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
			NLog.LogManager.Shutdown();
		}
	}
}
          