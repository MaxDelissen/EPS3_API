using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resources.Interfaces;
using Resources.Interfaces.DatabaseTables;
using Resources.Models;

namespace DAL;

public class UserRepository : IUserRepository
{
    private AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<List<User>> GetUsers() => _context.Users.ToListAsync();

    public Task<IUser> GetUser(int id) => throw new NotImplementedException();

    public Task<IUser> GetUser(string email) => throw new NotImplementedException();

    public Task<IUser> AddUser(IUser user) => throw new NotImplementedException();

    public Task<IUser> UpdateUser(IUser user) => throw new NotImplementedException();

    public Task<IUser> DeleteUser(int id) => throw new NotImplementedException();
}