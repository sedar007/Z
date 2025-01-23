using Common.DAO;
using Common.Request;
namespace DataAccess.Interface;

public interface IAuthDataAccess {
	Task<UserAuthDao?> GetUser(string username);
	Task<UserAuthDao> Create(UserAuthCreationRequest request);
}
