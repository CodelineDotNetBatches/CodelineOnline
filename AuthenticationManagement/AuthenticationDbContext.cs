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
            // default schema keeps everything under "courses"
            mb.HasDefaultSchema("Authentication");

            // Define relationships
            mb.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleID);

            //  Seed Roles
            //mb.Entity<Role>().HasData(
            //    new Role { RoleID = 1, RoleName = "Admin" },
            //    new Role { RoleID = 2, RoleName = "Instructor" },
            //    new Role { RoleID = 3, RoleName = "User" }
            //);

            base.OnModelCreating(mb);
        }
    }
}
