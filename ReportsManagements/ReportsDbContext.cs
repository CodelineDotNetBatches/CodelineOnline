using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements
{
    public class ReportsDbContext : DbContext
    {
        public DbSet<ReasonCode> ReasonCodes { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }

        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
        { }
        public DbSet<Models.Branch> Branches { get; set; }
        public DbSet<Models.Geolocation> Geolocations { get; set; }
        public DbSet<Models.BranchReport> BranchReports { get; set; }



        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("reports");

        }
    }
}
