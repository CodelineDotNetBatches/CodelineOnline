using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagement.Models
{
    [Index(nameof(ProgramName), IsUnique = true)]
    public class Programs
    {
        // Primary key
        [Key]
        public Guid ProgramId { get; set; }

        // Required, max length
        [Required, MaxLength(200)]
        public string ProgramName { get; set; } = null!;

        // description
        [MaxLength(500)]
        public string? ProgramDescription { get; set; }

        // URL to the program's roadmap (required)
        [Required, MaxLength(200)]
        [Url]
        public string Roadmap { get; set; } = null!;

        // Price 
        //[Column(TypeName = "decimal(10,2)")]
        //public decimal? Price { get; set; }

        // Timestamps
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships

        // Foreign key to Branch 
        // public int BranchId { get; set; }
        // public virtual Branch Branch { get; set; } = null!;

        // Foreign key to Admin 
        // public int AdminId { get; set; }
        // public virtual Admin Admin { get; set; } = null!;

        //// One-to-many: Program -> Enrollments
        //public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        //// Many-to-many: Program <-> Course
        //public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

        //// Many-to-many: Program <-> Category
        //public virtual ICollection<Category> Categories { get; set; } = new List<Category>();



}
}
