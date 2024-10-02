using Resources.Models;

namespace Resources.Interfaces;

public interface IUserRepository
{
    List<User> GetUsers();
    bool EmailExists(string email);

    /// <summary>
    /// Get a user by email
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when user not found</exception>
    User GetUserByEmail(string email);
    bool AddUser(User user);
}