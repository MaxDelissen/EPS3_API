using Microsoft.EntityFrameworkCore;

namespace Logic.Models
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public bool isSeller { get; set; }
    }
}