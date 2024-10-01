using Resources.Interfaces.DatabaseTables;
using Resources.Models;

namespace Resources.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<IUser> GetUser(int id);
    Task<IUser> GetUser(string email);
    Task<IUser> AddUser(IUser user);
    Task<IUser> UpdateUser(IUser user);
    Task<IUser> DeleteUser(int id);
}