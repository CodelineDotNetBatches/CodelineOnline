using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Configurations
{
    /// <summary>
    /// Fluent API configuration for <see cref="Lesson"/> entity.
    /// Defines table mapping, keys, and relationships.
    /// </summary>
    public class LessonConfig : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> b)
        {
            // Table name
            b.ToTable("Lessons");

            // Primary key
            b.HasKey(x => x.Id);

            // Title column: required, max length 200
            b.Property(x => x.Title).HasMaxLength(200).IsRequired();

            // Relationship: Lesson → Course (many-to-one)
            b.HasOne(x => x.Course)
             .WithMany(c => c.Lessons)
             .HasForeignKey(x => x.CourseId);
        }
    }

}
