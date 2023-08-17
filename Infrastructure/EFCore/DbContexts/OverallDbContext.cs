using EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFCore.DbContexts
{
    public class OverallDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Comment> Comments { get; set; } = null!;

        public DbSet<Coffee> Coffees { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public OverallDbContext(DbContextOptions<OverallDbContext> options, IConfiguration configuration) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasOne<Comment>().WithMany().HasForeignKey(k => k.CommentId);
            modelBuilder.Entity<Comment>().HasOne(c => c.Coffee).WithMany().HasForeignKey(k => k.CoffeeId);
            modelBuilder.Entity<Comment>().HasOne(c => c.User).WithMany().HasForeignKey(k => k.UserId);
            modelBuilder.Entity<RefreshToken>().HasOne(f => f.User).WithOne().HasForeignKey<RefreshToken>(f => f.UserId);
            
            
            modelBuilder.Entity<User>().ToTable(t => t
                .HasCheckConstraint(nameof(User.Login), $"LEN({nameof(User.Login)}) > 3 AND LEN({nameof(User.Login)}) < 15")
                .HasName("CK_User_Login_Length")
            );
        }

        
    }
}