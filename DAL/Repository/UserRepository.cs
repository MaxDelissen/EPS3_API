using Resources.Models.DbModels;

namespace DAL.Repository;

public class UserRepository : DirectDbRepository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<User> GetUsers() => _context.Users.ToList();

    public bool EmailExists(string email) => _context.Users.Any(u => u.Email == email);

    /// <summary>
    /// Get a user by email
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when user not found</exception>
    public User GetUserByEmail(string email) => _context.Users.FirstOrDefault(u => u.Email == email) ?? throw new InvalidOperationException("User not found");
    public User GetUserById(int id) => _context.Users.FirstOrDefault(u => u.Id == id) ?? throw new InvalidOperationException("User not found");

    public bool AddUser(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChanges() > 0;
    }
}