using Common.DAO;
using Common.Request;
namespace DataAccess.Interface;

public interface IAuthDataAccess {
	Task<UserAuthDao?> GetUser(string username);
	Task<UserAuthDao> Create(string username, string password, int userId);
	Task<UserAuthDao?> GetUserById(int id);
	
}
