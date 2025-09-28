using Microsoft.EntityFrameworkCore;

namespace AuthenticationManagement
{
    public class AuthenticationDbContext:DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
        : base(options)
        { }

      

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("courses");

        }
    }
}
