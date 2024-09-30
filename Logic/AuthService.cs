using Logic.Data;
using Logic.Models;
using Logic.Utilities;

namespace Logic;

public class AuthService(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext DbContext = dbContext;

    private bool IsEmailInUse(string registerRequestEmail) => dbContext.User.Any(u => u.email == registerRequestEmail);

    public string? CheckUserGenerteToken(string email, string password)
    {
        User? selectedUser = dbContext.User.FirstOrDefault(u => u.email == email);
        if (selectedUser == null)
            return null; // User not found

        if (!password.VerifyPassword(selectedUser.passwordHash))
            return null; // Password does not match

        string token = JwtGenerator.GenerateToken(selectedUser.id ,selectedUser.email, selectedUser.isSeller);
        return token;
    }

    public RegisterResponse RegisterUser(string registerRequestEmail, string registerRequestPassword, bool registerRequestIsSeller)
    {
        if (IsEmailInUse(registerRequestEmail))
            return new RegisterResponse { Result = RegisterResult.EmailInUse };

        string passwordHash = registerRequestPassword.HashPassword();
        User newUser = new User
        {
            email = registerRequestEmail,
            passwordHash = passwordHash,
            isSeller = registerRequestIsSeller
        };
        dbContext.User.Add(newUser);
        dbContext.SaveChanges();

        bool userAdded = dbContext.User.Any(u => u.email == registerRequestEmail);
        string token = JwtGenerator.GenerateToken(newUser.id, newUser.email, newUser.isSeller);
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