// Ensure UserService has a constructor that accepts an IUserRepository parameter
namespace Logic
{
    public class UserService(IUserRepository userRepository)
    {
        public string? GetUserNameById(int id)
        {
            try
            {
                User requestedUser = userRepository.GetUserById(id);
                return requestedUser.Email; //TODO: Change to return name, name is not yet in the User model
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }
    }
}