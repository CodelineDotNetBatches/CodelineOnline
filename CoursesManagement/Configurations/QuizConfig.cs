using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Configurations
{
    /// <summary>
    /// Fluent API configuration for <see cref="Quiz"/> entity.
    /// Defines table mapping, keys, and relationships.
    /// </summary>
    public class QuizConfig : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> b)
        {
            // Table name
            b.ToTable("Quizzes");

            // Primary key
            b.HasKey(x => x.Id);

            // Title column: required, max length 150
            b.Property(x => x.Title).HasMaxLength(150).IsRequired();

            // Relationship: Quiz → Lesson (many-to-one)
            b.HasOne(x => x.Lesson)
             .WithMany(l => l.Quizzes)
             .HasForeignKey(x => x.LessonId);
        }
    }

}
