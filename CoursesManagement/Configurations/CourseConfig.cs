using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Configurations
{
    /// <summary>
    /// Fluent API configuration for <see cref="Course"/> entity.
    /// Defines table mapping, keys, constraints, and relationships.
    /// </summary>
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> b)
        {
            // Table name
            b.ToTable("Courses");

            // Primary key
            b.HasKey(x => x.Id);

            // Title column: required, max length 200
            b.Property(x => x.Title).HasMaxLength(200).IsRequired();

            // Relationship: Course → Category (many-to-one)
            b.HasOne(x => x.Category)
             .WithMany(c => c.Courses)
             .HasForeignKey(x => x.CategoryId);
        }
    }


}
