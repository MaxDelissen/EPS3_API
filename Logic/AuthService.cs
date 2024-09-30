using Logic.Data;
using Logic.Models;
using Logic.Utilities;

namespace Logic;

public class AuthService(ApplicationDbContext dbContext)
{
    private readonly ApplicationDbContext DbContext = dbContext;

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

    public string? RegisterUser(string registerRequestEmail, string registerRequestPassword, bool registerRequestIsSeller)
    {
        throw new NotImplementedException();
    }
}