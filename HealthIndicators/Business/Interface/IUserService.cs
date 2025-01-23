using Common.DTO;
using Common.Request;

namespace Business.Interface;
public interface IUserService
{
    Task<UserDTO?> GetUserById(int id);
    Task<IEnumerable<UserDTO>> GetUsers();
    Task Remove(int id);
    Task<UserDTO> Create(UserCreationRequest request);
}