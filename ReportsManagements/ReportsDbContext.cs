using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements
{
    public class ReportsDbContext : DbContext
    {

        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
        { }
        public DbSet<Models.Branch> Branches { get; set; }
        public DbSet<Models.Geolocation> Geolocations { get; set; }
        public DbSet<Models.BranchReport> BranchReports { get; set; }
        public DbSet<ReasonCode> ReasonCodes { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("reports");

            // Seed initial data for ReasonCodes
            mb.Entity<ReasonCode>().HasData(
                new ReasonCode { ReasonCodeId = 1, Code = "LATE", Name = "Late", Category = "Attendance", IsActive = true },
                new ReasonCode { ReasonCodeId = 2, Code = "SICK", Name = "Sick", Category = "Health", IsActive = true },
                new ReasonCode { ReasonCodeId = 3, Code = "TECH", Name = "Technical Issue", Category = "System", IsActive = true }
            );

        }
    }
}


