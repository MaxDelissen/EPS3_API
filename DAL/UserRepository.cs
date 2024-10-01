using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resources.Interfaces;
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
}