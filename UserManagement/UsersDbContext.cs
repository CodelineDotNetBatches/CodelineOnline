using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UserManagement.Models;

namespace UserManagement
{
    public class UsersDbContext : DbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) 
        : base(options) 
        { }

        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<Batch> Batches { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            // default schema keeps everything under "users"
            mb.HasDefaultSchema("users");


            //// Optional: configure Batch
            //modelBuilder.Entity<Batch>(entity =>
            //{
            //    entity.HasKey(b => b.BatchId);
            //    entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
            //    entity.Property(b => b.Status).HasMaxLength(50);
            //});

            //// Optional: configure Trainee
            //modelBuilder.Entity<Trainee>(entity =>
            //{
            //    entity.HasKey(t => t.TraineeId);
            //    entity.Property(t => t.GithubUsername).IsRequired();
            //});

        }
    }
}
