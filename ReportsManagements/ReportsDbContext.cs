using Microsoft.EntityFrameworkCore;

namespace ReportsManagements
{
    public class ReportsDbContext : DbContext
    {
        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
        { }

       

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("reports");

        }
    }
}
