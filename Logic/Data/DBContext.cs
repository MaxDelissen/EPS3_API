using Logic.Models;
using Microsoft.EntityFrameworkCore;

namespace Logic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.passwordHash).HasColumnName("passwordHash");
                entity.Property(e => e.isSeller).HasColumnName("isSeller").HasConversion<short>();
            });
        }
    }
}