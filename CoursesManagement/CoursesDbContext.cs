using Microsoft.EntityFrameworkCore;
using CoursesManagement.Models;
namespace CoursesManagement
{
    /// <summary>
    /// Central EF Core database context for the Courses Management System.
    /// Defines tables, relationships, and Fluent API configurations
    /// not already covered by Data Annotations.
    /// </summary>
    public class CoursesDbContext : DbContext
    {
        public CoursesDbContext(DbContextOptions<CoursesDbContext> options)
            : base(options)
        { }
        // =========================
        // DbSet Tables
        // =========================
        public DbSet<Programs> Programs { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
        public DbSet<Course> Courses { get; set; } = default!;
        //public DbSet<User> Users { get; set; } = default!;
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Certificate> Certificates { get; set; } = default!;
        // =========================
        // Fluent API Configuration
        // =========================
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema (optional for organization)
            mb.HasDefaultSchema("courses");
            // ===================================================
            // PROGRAMS ENTITY
            // ===================================================
            mb.Entity<Programs>(entity =>
            {
                // Unique constraint on ProgramName
                entity.HasIndex(p => p.ProgramName).IsUnique();
            });
            // ===================================================
            // CATEGORY ENTITY
            // ===================================================
            mb.Entity<Category>(entity =>
            {
                // Unique constraint on CategoryName
                entity.HasIndex(c => c.CategoryName).IsUnique();
            });
            // ===================================================
            // COURSE ENTITY
            // ===================================================
            mb.Entity<Course>(entity =>
            {
                // Add index for faster course name search
                entity.HasIndex(c => c.CourseName);
                // Relationship: Course → Category (one-to-many)
                entity.HasOne(c => c.Category)
                      .WithMany(cat => cat.Courses)
                      .HasForeignKey(c => c.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            //// ===================================================
            //// USER ENTITY
            //// ===================================================
            //mb.Entity<User>(entity =>
            //{
            //    // Primary key and required constraints are already in Data Annotations.
            //    // We only add a unique index for Email if not present.
            //    entity.HasIndex(u => u.Email).IsUnique();
            //});
            // ===================================================
            // ENROLLMENT ENTITY
            // ===================================================
            mb.Entity<Enrollment>(entity =>
            {
                // Relationships
                //entity.HasOne(e => e.User)
                //      .WithMany(u => u.Enrollments)
                //      .HasForeignKey(e => e.UserId)
                //      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Program)
                      .WithMany(p => p.Enrollments)
                      .HasForeignKey(e => e.ProgramId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            // ===================================================
            // CERTIFICATE ENTITY
            // ===================================================
            mb.Entity<Certificate>(entity =>
            {
                //// Unique: one certificate per user per course
                //entity.HasIndex(c => new { c.UserId, c.CourseId }).IsUnique();
                // Unique: certificate URL must be unique
                entity.HasIndex(c => c.CertificateUrl).IsUnique();
            });
            // ===================================================
            // MANY-TO-MANY RELATIONSHIPS
            // ===================================================
            // Programs :left_right_arrow: Categories
            mb.Entity<Programs>()
              .HasMany(p => p.Categories)
              .WithMany(c => c.Programs)
              .UsingEntity(j => j.ToTable("ProgramCategories"));
            // Programs :left_right_arrow: Courses
            mb.Entity<Programs>()
              .HasMany(p => p.Courses)
              .WithMany(c => c.Programs)
              .UsingEntity(j => j.ToTable("ProgramCourses"));
            base.OnModelCreating(mb);
        }
    }
}