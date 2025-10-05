using Microsoft.EntityFrameworkCore;

using ReportsManagements.Models;
using System.Reflection.Emit;

namespace ReportsManagements
{
    public class ReportsDbContext : DbContext
    {

        public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
        { }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Geolocation> Geolocations { get; set; }
        public DbSet<BranchReport> BranchReports { get; set; }
        public DbSet<CourseReport> CourseReports { get; set; }
        public DbSet<TrainerReport> TrainerReports { get; set; }
        public DbSet<ReasonCode> ReasonCodes { get; set; }
        public DbSet<FileStorage> FileStorages { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecord { get; set; }
        public DbSet<GeoRadiusAudit> GeoRadiusAudits { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            // for AttendanceRecord 
            mb.Entity<AttendanceRecord>().HasKey(a => a.AttId);
            base.OnModelCreating(mb);

            mb.Entity<AttendanceRecord>()
                .HasOne(a => a.Geolocation)
                .WithMany(g => g.AttendanceRecords)
                .HasForeignKey(a => a.GeolocationId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<AttendanceRecord>()
                .HasOne(a => a.CapturedPhoto)
                .WithMany()
                .HasForeignKey(a => a.CapturedPhotoId)
               .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<AttendanceRecord>()
                .HasOne(a => a.ReasonCode)
                .WithMany()
                .HasForeignKey(a => a.ReasonCodeId)
                .OnDelete(DeleteBehavior.SetNull);

            mb.Entity<Geolocation>()
           .Property(g => g.RediusMeters)
          .HasPrecision(18, 2);


            mb.Entity<GeoRadiusAudit>()
              .HasKey(a => a.GeoRadiusAuditId);

            mb.Entity<GeoRadiusAudit>()
              .HasOne<Geolocation>()
              .WithMany()
              .HasForeignKey(a => a.GeolocationId)
              .OnDelete(DeleteBehavior.Cascade);


            // default schema keeps everything under "users"
            mb.HasDefaultSchema("reports");
            mb.Entity<Geolocation>().ToTable("Geolocations");

            // Seed initial data for ReasonCodes
            mb.Entity<ReasonCode>().HasData(
                new ReasonCode { ReasonCodeId = 1, Code = "LATE", Name = "Late", Category = "Attendance", IsActive = true },
                new ReasonCode { ReasonCodeId = 2, Code = "SICK", Name = "Sick", Category = "Health", IsActive = true },
                new ReasonCode { ReasonCodeId = 3, Code = "TECH", Name = "Technical Issue", Category = "System", IsActive = true }
            );

        }
    }
}


