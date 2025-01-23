using Business.Interface;
using Common.DAO;
using Common.DTO;
using Common.Request;
using DataAccess.Interface;
using Common.DTO.Helper;
using Microsoft.Extensions.Logging;

namespace Business.Implementation;
public class WellnessMetricsService : IWellnessMetricsService
{
    private readonly double KmToMilesFactor =  0.621371;
    private readonly IWellnessMetricsDataAccess _dataAccess;
    private readonly IUserDataAccess _userDataAccess;
    private readonly ILogger<WellnessMetricsService> _logger;
	
    public WellnessMetricsService(ILogger<WellnessMetricsService> logger, IWellnessMetricsDataAccess dataAccess, IUserDataAccess userDataAccess) {
        _logger = logger;
        _dataAccess = dataAccess;
        _userDataAccess = userDataAccess;
    }
    
    /*static double ConvertKmToMiles(double kilometers)
    {
        return kilometers * KmToMilesFactor;
    }

    // Méthode pour convertir des miles en kilomètres
    static double ConvertMilesToKm(double miles)
    {
        return miles / KmToMilesFactor;
    }*/

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