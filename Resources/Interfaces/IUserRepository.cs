using Resources.Models;

namespace Resources.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsers();
}