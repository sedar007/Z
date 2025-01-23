using Common.DAO;
using Common.Request;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation;

public class AuthDataAccess : IAuthDataAccess {
    
    private readonly HealthContext _context;
    
    public AuthDataAccess(HealthContext context) {
        this._context = context;
    }
    
    public Task<UserAuthDao?> GetUser(string username) {
        return _context.Authentification.FirstOrDefaultAsync(x => x.Username == username);
    }
    
    public async Task<UserAuthDao?> GetUserById(int id) {
        return await _context.Authentification.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    
    public async Task<UserAuthDao> Create(UserAuthCreationRequest request) {
        var newData = _context.Authentification.Add(new UserAuthDao {
            Username = request.Username,
            Password = request.Password,
            UserId = request.UserId
        });
        await _context.SaveChangesAsync();
        return await GetUserById(newData.Entity.Id) ?? throw new NullReferenceException("Erreur lors de la creation des données de santé");
    }

 
}

