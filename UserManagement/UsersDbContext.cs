using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        // Entities
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TraineeSkill> TraineeSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema keeps everything under "users"
            mb.HasDefaultSchema("users");

            //  many-to-many (Trainee <-> Skill)
            mb.Entity<TraineeSkill>()
              .HasKey(ts => new { ts.TraineeId, ts.SkillId });

            mb.Entity<TraineeSkill>()
              .HasOne(ts => ts.Trainee)
              .WithMany(t => t.TraineeSkills)
              .HasForeignKey(ts => ts.TraineeId);

            mb.Entity<TraineeSkill>()
              .HasOne(ts => ts.Skill)
              .WithMany(s => s.TraineeSkills)
              .HasForeignKey(ts => ts.SkillId);
        }
    }
}
