using Common.DAO;
using Common.Request;

namespace DataAccess.Interface;
public interface IUserDataAccess
{
    Task<UserDAO?> GetUserById(int id);
    Task<IEnumerable<UserDAO>> GetUsers();
    Task Remove(UserDAO user);
    Task<UserDAO> Create(UserCreationRequest request);
    Task<UserDAO?> GetUserByName(string name);
}