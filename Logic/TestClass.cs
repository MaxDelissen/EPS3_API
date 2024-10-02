using Resources.Interfaces;
using Resources.Models;

namespace Logic;

public class TestClass(IUserRepository userRepository)
{
    public List<User> GetUsers()
    {
        return userRepository.GetUsers();
    }
}