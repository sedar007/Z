using Common.DAO;
using Microsoft.EntityFrameworkCore;
using DataAccess.Interface;
using Common.Request;

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
    
    
    public async Task<UserAuthDao> Create(string username, string password, int userId) {
        var newData = _context.Authentification.Add(new UserAuthDao {
            Username = username,
            Password = password,
            UserId = userId
        });
        await _context.SaveChangesAsync();
        return await GetUserById(newData.Entity.Id) ?? throw new NullReferenceException("Erreur lors de la creation des données de santé");
    }

 
}

