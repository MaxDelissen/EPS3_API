using Resources.Models;

namespace Resources.Interfaces;

public interface IUserRepository
{
    List<User> GetUsers();
}