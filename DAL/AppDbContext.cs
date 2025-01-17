using Microsoft.EntityFrameworkCore;
using Resources.Models.DbModels;

namespace DAL
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuring Product entity relationships
            modelBuilder.Entity<Product>()
                .Navigation(p => p.ProductImages).AutoInclude();
            modelBuilder.Entity<Product>()
                .Navigation(p => p.ProductCategories).AutoInclude();

            modelBuilder.Entity<Order>().Property(o => o.Status)
                .HasConversion<string>();
            modelBuilder.Entity<Order>()
                .Navigation(o => o.OrderItems).AutoInclude();

            //Autoinclude categoryname for productcategory
            modelBuilder.Entity<ProductCategory>().Navigation(pc => pc.Category).AutoInclude();

            modelBuilder.Entity<OrderItem>()
                .Property(o => o.Quantity).HasDefaultValue(1);
        }
    }
}