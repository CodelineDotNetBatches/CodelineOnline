using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Configurations
{
    /// <summary>
    /// Fluent API configuration for <see cref="Category"/> entity.
    /// Defines table mapping, keys, and relationships.
    /// </summary>
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> b)
        {
            // Table name
            b.ToTable("Categories");

            // Primary key
            b.HasKey(x => x.Id);

            // Name column: required, max length 100
            b.Property(x => x.Name).HasMaxLength(100).IsRequired();

            // Relationship: Category → Programs (many-to-one)
            b.HasOne(x => x.Programs)
             .WithMany(p => p.Categories)
             .HasForeignKey(x => x.ProgramsId);
        }
    }

}
