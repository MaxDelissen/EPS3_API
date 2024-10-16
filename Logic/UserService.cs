// Ensure UserService has a constructor that accepts an IUserRepository parameter

using Resources.Interfaces.IRepository;

namespace Logic
{
    public class UserService(IUserRepository userRepository)
    {
        public string? GetUserNameById(int id)
        {
            try
            {
                User requestedUser = userRepository.GetUserById(id);
                return requestedUser.FullName;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }
    }
}