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
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Geolocation> Geolocations { get; set; }
        public DbSet<BranchReport> BranchReports { get; set; }
        public DbSet<CourseReport> CourseReports { get; set; }
        public DbSet<TrainerReport> TrainerReports { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("reports");

        }
    }
}


