using Common.DAO;
using Common.Request;
using DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Implementation;
public class UserDataAccess : IUserDataAccess
{
    private readonly HealthContext _context;
    
    public UserDataAccess(HealthContext context) {
        _context = context;
    }
    public async Task<UserDAO?> GetUserById(int id) {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<UserDAO?> GetUserByName(string name)
    {
        return _context.Users.FirstOrDefault(x => x.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<IEnumerable<UserDAO>> GetUsers() {
        return await _context.Users.ToListAsync();
    }
    
    public async Task Remove(UserDAO user) {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    public async Task<UserDAO> Create(UserCreationRequest request) {
        var newData = _context.Users.Add(new UserDAO {
            Name = request.Name,
            Age = request.Age,
            Weight = request.Weight,
            Height = request.Height
        });
        await _context.SaveChangesAsync();
        return await GetUserById(newData.Entity.Id) ?? throw new NullReferenceException("Erreur lors de la creation des données de santé");
    }
}