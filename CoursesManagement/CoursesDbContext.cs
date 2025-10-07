using Microsoft.EntityFrameworkCore;
using CoursesManagement.Models;
using CoursesManagement.Data;

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
        public DbSet<Enrollment> Enrollments { get; set; } = default!;
        public DbSet<Certificate> Certificates { get; set; } = default!;

        // =========================
        // Fluent API Configuration
        // =========================
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Default schema for organization
            mb.HasDefaultSchema("courses");

            // ===================================================
            // PROGRAMS ENTITY
            // ===================================================
            mb.Entity<Programs>(entity =>
            {
                entity.HasIndex(p => p.ProgramName).IsUnique();
            });

            // ===================================================
            // CATEGORY ENTITY
            // ===================================================
            mb.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.CategoryName).IsUnique();
            });

            // ===================================================
            // COURSE ENTITY
            // ===================================================
            mb.Entity<Course>(entity =>
            {
                entity.HasIndex(c => c.CourseName);
                entity.HasOne(c => c.Category)
                      .WithMany(cat => cat.Courses)
                      .HasForeignKey(c => c.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ===================================================
            // ENROLLMENT ENTITY
            // ===================================================
            mb.Entity<Enrollment>(entity =>
            {
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
                // Unique: Certificate URL must be unique
                entity.HasIndex(c => c.CertificateUrl).IsUnique();

                // Relationship: Certificate → Course
                entity.HasOne(c => c.Course)
                      .WithMany() // no back-collection needed
                      .HasForeignKey(c => c.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship: Certificate → Enrollment
                // 🚫 Prevent multiple cascade path (the key fix)
                entity.HasOne(c => c.Enrollment)
                      .WithMany(e => e.Certificates)
                      .HasForeignKey(c => c.EnrollmentId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ===================================================
            // MANY-TO-MANY RELATIONSHIPS
            // ===================================================
            mb.Entity<Programs>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Programs)
                .UsingEntity<Dictionary<string, object>>(
                    "ProgramCategories",
                    j => j.HasOne<Category>().WithMany().HasForeignKey("CategoriesCategoryId"),
                    j => j.HasOne<Programs>().WithMany().HasForeignKey("ProgramsProgramId"),
                    j =>
                    {
                        j.HasKey("ProgramsProgramId", "CategoriesCategoryId"); // ✅ Now correctly inside
                        j.ToTable("ProgramCategories");
                    });

            mb.Entity<Programs>()
              .HasMany(p => p.Courses)
              .WithMany(c => c.Programs)
              .UsingEntity(j => j.ToTable("ProgramCourses"));

            base.OnModelCreating(mb);

            // Seed static data
            SeedData.Seed(mb);
        }
    }
}
