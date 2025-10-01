using AuthenticationManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationManagement
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options) { }

        // DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("users");

            // Define relationships
            mb.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

            base.OnModelCreating(mb);
        }
    }
}
