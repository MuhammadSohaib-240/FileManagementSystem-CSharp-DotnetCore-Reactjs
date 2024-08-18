using fms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace fms.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Files)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId);
        }
    }
}
