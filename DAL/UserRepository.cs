using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Resources.Interfaces;
using Resources.Models;

namespace DAL;

public class UserRepository : IUserRepository
{
    private AppDbContext _context;

    public UserRepository(IConfiguration configuration)
    {
        _context = new AppDbContext(new AppConfiguration(configuration));
    }
    
    public List<User> GetUsers() => _context.Users.ToListAsync().Result;
}