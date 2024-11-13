using Resources.Models.DbModels;

namespace Resources.Interfaces.IRepository;

public interface IUserRepository
{
    List<User> GetUsers();
    bool EmailExists(string email);

    /// <summary>
    /// Get a user by email
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when user not found</exception>
    User GetUserByEmail(string email);
    User GetUserById(int id);
    bool AddUser(User user);
}