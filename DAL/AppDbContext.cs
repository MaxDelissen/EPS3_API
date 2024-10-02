using Microsoft.EntityFrameworkCore;
using Resources.Models;

namespace DAL
{
    //dotnet ef migrations add {Name} --project DAL --startup-project API //Replace {Name} with the name of the migration
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;
        public AppDbContext(AppConfiguration appConfiguration)
        {
            _connectionString = appConfiguration.GetConnectionString();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_connectionString);
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
