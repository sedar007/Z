using Common.DTO;
using Common.Request;
using Common.Response;

namespace Business.Interface;
public interface IUserService
{
    Task<UserDTO?> GetUserById(int id);
    Task<UserDTO?> GetUserByName(string name);
    Task<UserLast7StepsResponse> GetLast7DaysSteps(int userId);
    Task<UserLast7DistancesResponse> GetLast7DaysDistances(int userId);
    Task<IEnumerable<UserDTO>> GetUsers();
    Task Remove(int id);
    Task<UserDTO> Create(UserCreationRequest request);
    
    
}