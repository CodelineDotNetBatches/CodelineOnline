using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoursesManagement.Models;

namespace CoursesManagement.Configurations
{
    /// <summary>
    /// Fluent API configuration for <see cref="Programs"/> entity.
    /// Defines table mapping, keys, and constraints.
    /// </summary>
    public class ProgramsConfig : IEntityTypeConfiguration<Programs>
    {
        public void Configure(EntityTypeBuilder<Programs> b)
        {
            // Table name
            b.ToTable("Programs");

            // Primary key
            b.HasKey(x => x.Id);

            // Title column: max length 200, required
            b.Property(x => x.Title).HasMaxLength(200).IsRequired();
        }
    }


}
