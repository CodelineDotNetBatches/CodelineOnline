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
        public DbSet<TraineeSkill> TraineeSkills { get; set; }
        public DbSet<InstructorSkill> InstructorSkills { get; set; }
        public DbSet<Admin_Profile> AdminProfiles { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }


        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema
            mb.HasDefaultSchema("users");

            // ==========================================

            // 1. Instructor entity configuration

            //==========================================

          
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
                // indexing by batch name 
                entity.HasIndex(b => b.BatchName).IsUnique();
            });


            // ==================================================
            // 4. Admin_Resposibilities entity configuration
            // ==================================================

            // Composite key
            mb.Entity<Responsibility>()

                .HasKey(r => new { r.AdminId, r.ResponsibilityTitle});
            // indexing by responsibility name
            mb.Entity<Responsibility>()
                .HasIndex(r => r.ResponsibilityTitle).IsUnique();

            // ==================================================
            // 5. TraineeSkills entity configuration
            // ==================================================
            mb.Entity<TraineeSkill>()
                .HasIndex(TS => TS.SkillName);
            // Composite key
            mb.Entity<TraineeSkill>()
                .HasKey(ts => new { ts.TraineeId, ts.TraineeSkillId }); // to not allow duplicate skills for a trainee


            // ==================================================
            // 6. InstructorSkills entity configuration
            // ==================================================
            mb.Entity<InstructorSkill>()
                .HasIndex(IS => IS.SkillName);

            // Composite key
            mb.Entity<InstructorSkill>()
                .HasKey(isk => new { isk.InstructorId, isk.InstructorSkillId }); // to not allow duplicate skills for an instructor














            // ==============================================
            // 7. Seed data 

            //====================================

            InstructorsSeedData.InstructorsSeed(mb);
            AvailabilitiesSeedData.AvailabilitiesSeed(mb);


        }
    }
}
