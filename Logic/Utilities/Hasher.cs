namespace Logic.Utilities;

public static class Hasher
{
    public static string HashPassword(this string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public static bool VerifyPassword(this string password, string passwordHash) =>
        BCrypt.Net.BCrypt.Verify(password, passwordHash);
}