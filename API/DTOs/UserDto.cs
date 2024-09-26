namespace API.DTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsSeller { get; set; }
    }
}