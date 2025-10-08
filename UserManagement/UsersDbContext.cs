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
        // DbSet Declarations
        // ===============================================
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<TraineeSkill> TraineeSkills { get; set; }
        public DbSet<InstructorSkill> InstructorSkills { get; set; }
        public DbSet<Admin_Profile> AdminProfiles { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Branch> branchs { get; set; }
        public DbSet<BranchPN> branchPNs { get; set; }
        public DbSet<Room> rooms  { get; set; }



        // ===============================================
        // OnModelCreating Configuration
        // ===============================================
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema
            mb.HasDefaultSchema("users");

            // ==================================================
            // 1️⃣ Admin Profile Configuration + Seed
            // ==================================================
            mb.Entity<Admin_Profile>().HasKey(a => a.AdminId);

            // ✅ Add explicit seeding for Admins
            mb.Entity<Admin_Profile>().HasData(
                new Admin_Profile { AdminId = 1 },
                new Admin_Profile { AdminId = 2 },
                new Admin_Profile { AdminId = 3 }
            );

            // ==================================================
            // 2️⃣ Instructor Configuration
            // ==================================================
            mb.Entity<Instructor>()
                .HasIndex(i => new { i.Specialization });

            mb.Entity<Instructor>()
                .Property(x => x.Experience_Level)
                .HasConversion<int>();

            mb.Entity<Instructor>()
                .Property(x => x.Teaching_Style)
                .HasConversion<int>();

            // ==================================================
            // 3️⃣ Availability Configuration
            // ==================================================
            mb.Entity<Availability>()
                .HasKey(a => new { a.InstructorId, a.time });

            mb.Entity<Availability>()
                .HasIndex(a => new { a.avilabilityId }).IsUnique();

            mb.Entity<Availability>()
                .HasIndex(a => a.Avail_Status);

            mb.Entity<Availability>()
                .Property(x => x.Avail_Status).HasConversion<int>();
            mb.Entity<Availability>()
                .Property(x => x.day_of_week).HasConversion<int>();

            // ==================================================
            // 4️⃣ Batch Configuration
            // ==================================================
            mb.Entity<Batch>(entity =>
            {
                entity.HasIndex(b => b.BatchName).IsUnique();
            });

            // ==================================================
            // 5️⃣ Responsibility Configuration
            // ==================================================
            mb.Entity<Responsibility>()
                .HasKey(r => new { r.AdminId, r.ResponsibilityTitle });

            mb.Entity<Responsibility>()
                .HasIndex(r => r.ResponsibilityTitle)
                .IsUnique();

            // ==================================================
            // 6️⃣ TraineeSkills Configuration
            // ==================================================
            mb.Entity<TraineeSkill>()
                .HasIndex(ts => ts.TraineeSkillId);

            mb.Entity<TraineeSkill>()
                .HasKey(ts => new { ts.TraineeId, ts.SkillName });

            // ==================================================
            // 7️⃣ InstructorSkills Configuration
            // ==================================================
            mb.Entity<InstructorSkill>()
                .HasIndex(isk => isk.InstructorSkillId);

            mb.Entity<InstructorSkill>()
                .HasKey(isk => new { isk.InstructorId, isk.SkillName});


            //===========================
            //8. branch phone
            //===============
            mb.Entity<BranchPN>()
               .HasKey(pn => new { pn.BranchId, pn.PhoneNumber });


            // ==================================================
            // 8️⃣ Other Seeds
            // ==================================================
            //InstructorsSeedData.InstructorsSeed(mb);
            //AvailabilitiesSeedData.AvailabilitiesSeed(mb);
            //BranchSeed.Seed(mb);
            //AdminProfileSeed.Seed(mb);
            



        }
    }
}
