using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
using UserManagement.SeedData;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }

        // Entities
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchTrainee> BatchTrainees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TraineeSkill> TraineeSkills { get; set; }


        public DbSet<Admin_Profile> AdminProfiles { get; set; }

        public DbSet<Responsibility> Responsibilities { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema
            mb.HasDefaultSchema("users");

            // Batch config
            mb.Entity<Batch>(entity =>
            {
                entity.HasKey(b => b.BatchId);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Status).HasMaxLength(50);
            });

            // Trainee config
            mb.Entity<Trainee>(entity =>
            {
                entity.HasKey(t => t.TraineeId);
                entity.Property(t => t.GithubUsername).IsRequired();
            });

            // BatchTrainee config (composite PK)
            mb.Entity<BatchTrainee>(entity =>
            {
                entity.HasKey(bt => new { bt.BatchId, bt.TraineeId });

                entity.HasOne(bt => bt.Batch)
                      .WithMany(b => b.BatchTrainees)
                      .HasForeignKey(bt => bt.BatchId);

                entity.HasOne(bt => bt.Trainee)
                      .WithMany(t => t.BatchTrainees)
                      .HasForeignKey(bt => bt.TraineeId);
            });

            // TraineeSkill config (many-to-many Trainee <-> Skill)
            mb.Entity<TraineeSkill>(entity =>
            {
                entity.HasKey(ts => new { ts.TraineeId, ts.SkillId });

                entity.HasOne(ts => ts.Trainee)
                      .WithMany(t => t.TraineeSkills)
                      .HasForeignKey(ts => ts.TraineeId);

                entity.HasOne(ts => ts.Skill)
                      .WithMany(s => s.TraineeSkills)
                      .HasForeignKey(ts => ts.SkillId);
            });

            // Call external seed data
            mb.SeedData();
        }
    }
}
