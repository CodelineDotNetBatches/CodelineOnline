using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
using UserManagement.SeedData;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options) { }
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        // Entities
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<BatchTrainee> BatchTrainees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");

            // Batch config
            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasKey(b => b.BatchId);
                entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
                entity.Property(b => b.Status).HasMaxLength(50);
            });

            // Trainee config
            modelBuilder.Entity<Trainee>(entity =>
            {
                entity.HasKey(t => t.TraineeId);
                entity.Property(t => t.GithubUsername).IsRequired();
            });

            // BatchTrainee config (composite PK)
            modelBuilder.Entity<BatchTrainee>(entity =>
            {
                entity.HasKey(bt => new { bt.BatchId, bt.TraineeId });

                entity.HasOne(bt => bt.Batch)
                      .WithMany(b => b.BatchTrainees)
                      .HasForeignKey(bt => bt.BatchId);
        public DbSet<Skill> Skills { get; set; }
        public DbSet<TraineeSkill> TraineeSkills { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema keeps everything under "users"
            mb.HasDefaultSchema("users");

                entity.HasOne(bt => bt.Trainee)
                      .WithMany(t => t.BatchTrainees)
                      .HasForeignKey(bt => bt.TraineeId);
            });
            // 👇 Call external seed data
            modelBuilder.SeedData();
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
