using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using System.Reflection.Emit;

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
        public DbSet<Models.AttendanceRecord> AttendanceRecord { get; set; }



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


            // default schema keeps everything under "users"
            mb.HasDefaultSchema("reports");
            mb.Entity<Geolocation>().ToTable("Geolocations");

        }
    }
}


