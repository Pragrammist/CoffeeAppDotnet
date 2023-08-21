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
        public OverallDbContext(DbContextOptions<OverallDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        
    }
}