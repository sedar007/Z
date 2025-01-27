using Business.Interface;
using Business.Tools;
using Common.DAO;
using Common.DTO;
using Common.DTO.Helper;
using Common.Request;
using Common.Response;
using DataAccess.Interface;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Business.Implementation;
public class WellnessMetricsService : IWellnessMetricsService
{
    private readonly IWellnessMetricsDataAccess _dataAccess;
    private readonly IUserDataAccess _userDataAccess;
    private readonly ILogger<WellnessMetricsService> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IAuthService _authService;
    
    public WellnessMetricsService(ILogger<WellnessMetricsService> logger, IWellnessMetricsDataAccess dataAccess,
        IUserDataAccess userDataAccess
        , IHttpContextAccessor httpContextAccessor, IAuthService authService) {
        _logger = logger;
        _dataAccess = dataAccess;
        _userDataAccess = userDataAccess;
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }
  
    public async Task<WellnessMetricsResponse?> GetWellnessMetricsById(int id, string unit = "km") {
    try {
        var wellnessMetricsDao = await _dataAccess.GetWellnessMetricsById(id);
        if (wellnessMetricsDao == null) throw new InvalidDataException("Invalid Data");

        return await CreateWellnessMetricsResponse(wellnessMetricsDao, unit);
    } catch (Exception e) {
        _logger.LogError(e, e.Message);
        throw;
    }
}

public async Task<WellnessMetricsResponse?> GetWellnessMetricsTodayByUserId(int idAuth, string unit = "km") {
    try {
        var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId == null || userId != idAuth.ToString()) throw new UnauthorizedAccessException("Access denied");

        var user = await _authService.GetUserById(idAuth);
        if(user == null) throw new InvalidDataException("Invalid Data");
        var wellnessMetricsDao = await _dataAccess.GetWellnessMetricsTodayByUserId(user.IdUser);
        if (wellnessMetricsDao == null) throw new InvalidDataException("Invalid Data");

        return await CreateWellnessMetricsResponse(wellnessMetricsDao, unit);
    } catch (Exception e) {
        _logger.LogError(e, e.Message);
        throw;
    }
}

private async Task<WellnessMetricsResponse?> CreateWellnessMetricsResponse(WellnessMetricsDAO wellnessMetricsDao, string unit) {
    WellnessMetricsDTO wellnessMetricsDto = wellnessMetricsDao.ToDto();
    var user = await _userDataAccess.GetUserById(wellnessMetricsDto.UserId);
    if (user == null) throw new InvalidDataException("User not found");

    float distance = Converter.StepsToKm(user.Height, wellnessMetricsDto.Steps);
    float distanceUnit = (unit != "km") ? Converter.KmToMiles(distance) : distance;
    float bmi = user.Weight / (user.Height * user.Height);

    return new WellnessMetricsResponse {
        IdUser = wellnessMetricsDto.UserId,
        Steps = wellnessMetricsDto.Steps,
        SleepDuration = wellnessMetricsDto.SleepDuration,
        HeartRate = wellnessMetricsDto.HeartRate,
        Distance = distanceUnit,
        Bmi = bmi,
        Date = wellnessMetricsDto.Date,
        Weight = user.Weight,
        Height = user.Height,
        CategoryImc = Converter.GetCategoryBmi(bmi)
    };
}
    
    
    public async Task<WellnessMetricsDTO> Create(WellnessMetricsCreationRequest request) {
        try {
            if (request == null) throw new InvalidDataException("Invalid request");
            UserDAO? userDao = await _userDataAccess.GetUserById(request.IdUser); 
            if( userDao == null) throw new InvalidDataException("User not found");
            return (await _dataAccess.Create(request)).ToDto();
        } catch (Exception e) {
            _logger.LogError(e, e.Message);
            throw;
        }
    }
}