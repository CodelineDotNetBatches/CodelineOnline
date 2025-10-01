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
        public DbSet<Instructor> Instructors => Set<Instructor>();
        public DbSet<Availability> Availabilities => Set<Availability>();
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("users");

            /// Instructor entity configuration

            // Unique GitHub username if provided
            mb.Entity<Instructor>()
            .HasIndex(i => i.GithubUserName)
            .IsUnique();

            mb.Entity<Instructor>()
                .ToTable(t => t.HasCheckConstraint(
                "CK_Instructor_ExperienceLevel",
                "Experience_Level in ('Junior','Mid','Senior','Lead')"));
            mb.Entity<Instructor>()
                .ToTable(t => t.HasCheckConstraint(
                "CK_Instructor_TeachingStyle",
                "Teaching_Style in ('Project-based','Theory-first','Hands-on','Lecture','Discussion')"));

            // optional: index for specialization + experience
            mb.Entity<Instructor>()
                .HasIndex(i => new
                {
                    i.Specialization
                });

            // Enum to int conversions
            mb.Entity<Instructor>()
                .Property(x => x.Experience_Level).HasConversion<int>();
            mb.Entity<Instructor>()
                .Property(x => x.Teaching_Style).HasConversion<int>();



            /// Availability entity configuration

            // Availability composite key
            mb.Entity<Availability>()
            .HasKey(a => new { a.InstructorId, a.avilabilityId });

            // Validate day_of_week by check constraint (SQL Server / PostgreSQL)
            mb.Entity<Availability>()
            .ToTable(t => t.HasCheckConstraint(
            "CK_Availability_DayOfWeek",
            "day_of_week in ('Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sunday')"));

            mb.Entity<Availability>()
                .ToTable(t => t.HasCheckConstraint(
                    "CK_Availability_AvailStatus",
                    "Avail_Status in ('Active','Inactive','Complete','Busy')"));

            mb.Entity<Availability>()
                .HasIndex(a => a.Avail_Status);


            // Enum to int conversions

            mb.Entity<Availability>()
                .Property(x => x.Avail_Status).HasConversion<int>();
            mb.Entity<Availability>()
                .Property(x => x.day_of_week).HasConversion<int>();


            // Seed data for Instructors



            InstructorsSeedData.InstructorsSeed(mb);
            AvailabilitiesSeedData.AvailabilitiesSeed(mb);


        }
    }
}
