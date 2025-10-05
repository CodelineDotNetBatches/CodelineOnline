using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserManagement.Models;
using UserManagement.SeedData;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }
        // ===============================================       
        // DbSet
        // =======================================

        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Batch> Batches { get; set; }
     
        public DbSet<Skill> Skills { get; set; }
        

        public DbSet<Admin_Profile> AdminProfiles { get; set; }

        public DbSet<Responsibility> Responsibilities { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema
            mb.HasDefaultSchema("users");
            // ==========================================

            // 1. nstructor entity configuration

            //==========================================

            // Unique GitHub username if provided
            mb.Entity<Instructor>()
            .HasIndex(i => i.GithubUserName)
            .IsUnique();

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

            // ===========================================

            // 2. Availability entity configuration

            // ===========================================

            // Availability composite key
            mb.Entity<Availability>()
            .HasKey(a => new { a.InstructorId, a.avilabilityId });

          
            mb.Entity<Availability>()
                .HasIndex(a => a.Avail_Status);


            // Enum to int conversions

            mb.Entity<Availability>()
                .Property(x => x.Avail_Status).HasConversion<int>();
            mb.Entity<Availability>()
                .Property(x => x.day_of_week).HasConversion<int>();

            // ================================================
            // 3. Batch entity configuration

            // ================================================
            mb.Entity<Batch>(entity =>
            {
                entity.HasKey(b => b.BatchId);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Status).HasMaxLength(50);
            });

            // ==================================================
            // 4. Trainee entity configuration
            // ==================================================

            // Trainee config
            mb.Entity<Trainee>(entity =>
            {
                entity.HasKey(t => t.TraineeId);
                entity.Property(t => t.GithubUsername).IsRequired();
            });

            // ==================================================
            // 5. BatchTrainee entity configuration
            // ==================================================

           

            // ==================================================
            // 6. TraineeSkill entity configuration
            // ==================================================

            


            // ==============================================
            // Seed data 

            //====================================

            InstructorsSeedData.InstructorsSeed(mb);
            AvailabilitiesSeedData.AvailabilitiesSeed(mb);
            
            
        }
    }
}
