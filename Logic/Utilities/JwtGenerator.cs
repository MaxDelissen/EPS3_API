using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Utilities;

public static class JwtGenerator
{
    public static string Key = "blank";

    public static string GenerateToken(int id, string email, bool isSeller, int expirationTime = 7)
    {
        if (Key == null || Key == "blank") //Key was not set, or is default (which should have been overwritten on startup)
            throw new Exception("Key is null");
        byte[] key = Encoding.ASCII.GetBytes(Key);

        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, isSeller ? "Seller" : "Buyer")
        };

        var token = new JwtSecurityToken(
        issuer: "LoginManager",
        audience: "User",
        claims: claims,
        expires: DateTime.UtcNow.AddDays(expirationTime),
        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return tokenHandler.WriteToken(token);
    }
}