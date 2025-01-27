using Business.Interface;
using Business.Tools;
using Common.DTO;
using Common.DTO.Helper;
using Common.Request;
using Common.Response;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;

namespace Business.Implementation;
public class UserService : IUserService
{
    private readonly IUserDataAccess _dataAccess;
    private readonly IWellnessMetricsDataAccess _wellnessMetricsDataAccess;
    private readonly IAuthService _authService;
    private readonly ILogger<UserService> _logger;
	
    public UserService(ILogger<UserService> logger, IUserDataAccess dataAccess, IAuthService authService, IWellnessMetricsDataAccess wellnessMetricsDataAccess) {
        _logger = logger;
        _dataAccess = dataAccess;
        _authService = authService;
        _wellnessMetricsDataAccess = wellnessMetricsDataAccess;
    }
    
    public async Task<UserDTO?> GetUserById(int id) {
        try {
            return (await _dataAccess.GetUserById(id))?.ToDto();
        } catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
    
    public async Task<UserDTO?> GetUserByName(string name) {
        try {
            return (await _dataAccess.GetUserByName(name))?.ToDto();
        } catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
    
    public async Task<IEnumerable<UserDTO>> GetUsers() {
        try {
            return (await _dataAccess.GetUsers())
                .Select(userDao => userDao.ToDto());
        } catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
    
    public async Task Remove(int id) {
        try {
            var user = await _dataAccess.GetUserById(id);
            if (user is null) throw new InvalidDataException("User not found");
            await _dataAccess.Remove(user);
        } catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
    
    public async Task<UserDTO> Create(UserCreationRequest request) {
        try {
            var user = await _dataAccess.GetUserByName(request.Name);
            if (user != null)
                throw new InvalidDataException("User already exists");
            
            string error = Validator(request);
            if (string.IsNullOrEmpty(error) == false) throw new InvalidDataException(error);
            if (request.UnitWeight == "lb") {
                float convertedWeight = Converter.LbToKg(request.Weight);
                request.Weight = convertedWeight;
            }
            
            var response = (await _dataAccess.Create(request)).ToDto();
            await _authService.Create(response.Name, request.Password, response.Id);
            return response;
        } catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    public async Task<UserLast7StepsResponse> GetLast7DaysSteps(int userId) {
        try { 
            return (await _wellnessMetricsDataAccess.GetUserLast7DaysSteps(userId));
        }
        catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
    
    public async Task<UserLast7DistancesResponse> GetLast7DaysDistances(int userId) {
        try {
            var metrics = await _wellnessMetricsDataAccess.GetWellnessMetrics7DaysByUserId(userId);
        
            if (metrics == null ) 
                throw new InvalidDataException("Metrics are null");
            
            var maxStepsPerDay = metrics
                .GroupBy(metric => metric.Date.Date) 
                .Select(group => new {
                    Date = group.Key.ToString("yyyy-MM-dd"),
                    MaxSteps = group.Max(metric => metric.Steps) 
                }).ToList();

            var user = await _dataAccess.GetUserById(userId);
            if (user == null) 
                throw new InvalidDataException("User is null");
            
            var height = user.Height;
            
            var response = new UserLast7DistancesResponse {
                Distances = maxStepsPerDay.Select(entry => new Dictionary<string, object>
                    { 
                        { "date", entry.Date },
                        { "distances", Converter.StepsToKm(height, entry.MaxSteps) }
                    }).ToList(),
                
                TotalDistances = maxStepsPerDay.Sum(entry => Converter.StepsToKm(height, entry.MaxSteps)) 
            };
            return response;
        }
        catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }

    private string Validator(UserCreationRequest? request) {
        if (request == null) return "Invalid request.";
        if (string.IsNullOrWhiteSpace(request.Name)) return "Name cannot be empty.";
        if (request.Name.Length < 3 || request.Name.Length > 50) return "Name must be between 3 and 50 characters.";
        if (request.Age < 0 || request.Age > 120) return "Age must be between 1 and 120 years.";
        if (request.Weight < 0) return "Weight must be greater than 0 kg.";
        if (request.Height < 0 || request.Height > 5) return "Height must be greater than 0 and less than or equal to 5 meters.";
        return string.Empty;
    }
}