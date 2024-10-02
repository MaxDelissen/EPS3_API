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

    public List<User> GetUsers() => _context.Users.ToList();

    public bool EmailExists(string email) => _context.Users.Any(u => u.Email == email);

    /// <summary>
    /// Get a user by email
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when user not found</exception>
    public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new InvalidOperationException("User not found");

    public bool AddUser(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChanges() > 0;
    }
}