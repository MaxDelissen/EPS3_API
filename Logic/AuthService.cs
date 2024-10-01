/*using Logic.Interfaces;
using Logic.Utilities;
using Resources.Models;

namespace Logic;

public class AuthService(AppDbContext dbContext)
{
    private readonly AppDbContext DbContext = dbContext;

    private bool IsEmailInUse(string registerRequestEmail) => dbContext.User.Any(u => u.email == registerRequestEmail);

    public string? CheckUserGenerteToken(string email, string password)
    {
        User? selectedUser = dbContext.User.FirstOrDefault(u => u.email == email);
        if (selectedUser == null)
            return null; // User not found

        if (!password.VerifyPassword(selectedUser.PasswordHash))
            return null; // Password does not match

        string token = JwtGenerator.GenerateToken(selectedUser.Id ,selectedUser.Email, selectedUser.IsSeller);
        return token;
    }

    public RegisterResponse RegisterUser(string registerRequestEmail, string registerRequestPassword, bool registerRequestIsSeller)
    {
        if (IsEmailInUse(registerRequestEmail))
            return new RegisterResponse { Result = RegisterResult.EmailInUse };

        string passwordHash = registerRequestPassword.HashPassword();
        User newUser = new User
        {
            Email = registerRequestEmail,
            PasswordHash = passwordHash,
            IsSeller = registerRequestIsSeller
        };
        dbContext.User.Add(newUser);
        dbContext.SaveChanges();

        bool userAdded = dbContext.User.Any(u => u.email == registerRequestEmail);
        string token = JwtGenerator.GenerateToken(newUser.Id, newUser.Email, newUser.IsSeller);
        return new RegisterResponse
        {
            Result = userAdded ? RegisterResult.Success : RegisterResult.Failure,
            Token = userAdded ? token : null
        };

        public void getUser(int id)
        {

        }


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
}*/