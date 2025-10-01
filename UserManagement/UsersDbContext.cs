using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserManagement.Models;
using UserManagement.SeedData;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) 
        : base(options) 
        { }

        public DbSet<Trainee> Trainees { get; set; }
        
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("users");

            


        }
    }
}
