using Resources.Models.DbModels;

namespace Logic;

public class AuthService
{
    private readonly IUserRepository userRepository;
    public AuthService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public string? CheckUserGenerateToken(string email, string password)
    {
        if(!userRepository.EmailExists(email))
            return null; // Email does not exist

        User selectedUser = userRepository.GetUserByEmail(email);

        if (!password.VerifyPassword(selectedUser.PasswordHash))
            return null; // Password does not match

        string token = JwtGenerator.GenerateToken(selectedUser.Id ,selectedUser.Email, selectedUser.IsSeller);
        return token;
    }

    public RegisterResponse RegisterUser(string fullName, string email, string password, bool isSeller)
    {
        if (userRepository.EmailExists(email))
            return new RegisterResponse { Result = RegisterResult.EmailInUse };

        string passwordHash = password.HashPassword();
        User newUser = new User
        {
            FullName = fullName,
            Email = email,
            PasswordHash = passwordHash,
            IsSeller = isSeller
        };
        userRepository.AddUser(newUser);

        bool userAdded = userRepository.EmailExists(email);
        string token = JwtGenerator.GenerateToken(newUser.Id, newUser.Email, newUser.IsSeller);
        return new RegisterResponse
        {
            Result = userAdded ? RegisterResult.Success : RegisterResult.Failure,
            Token = userAdded ? token : null
        };
    }

    public enum RegisterResult
    {
        Success,
        EmailInUse,
        Failure
    }

    public class RegisterResponse
    {
        public RegisterResult Result { get; set; }
        public string? Token { get; set; }
    }
}