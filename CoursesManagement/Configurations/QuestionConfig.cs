using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Configurations
{
    /// <summary>
    /// Fluent API configuration for <see cref="Question"/> entity.
    /// Defines table mapping, keys, and relationships.
    /// </summary>
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> b)
        {
            // Table name
            b.ToTable("Questions");

            // Primary key
            b.HasKey(x => x.Id);

            // Text column: required
            b.Property(x => x.Text).IsRequired();

            // Relationship: Question → Quiz (many-to-one)
            b.HasOne(x => x.Quiz)
             .WithMany(q => q.Questions)
             .HasForeignKey(x => x.QuizId);
        }
    }

}
