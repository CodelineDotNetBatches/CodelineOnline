using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) 
        : base(options) 
        { }

        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<Admin_Profile> AdminProfiles { get; set; }

        public DbSet<Responsibility> Responsibilities { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("users");

        }
    }
}
