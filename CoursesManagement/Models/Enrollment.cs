using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a student's enrollment in a specific course (and optionally program).
    /// </summary>
    public class Enrollment
    {
        // =============================
        // Primary Key
        // =============================
        [Key]
        public Guid EnrollmentId { get; set; }

        // =============================
        // Relationships: User (Student)
        // =============================
        [Required]
        public Guid UserId { get; set; }

        // Uncomment later when User entity is ready
        //[ForeignKey(nameof(UserId))]
        //public virtual User User { get; set; } = default!;

        // =============================
        // Relationships: Course
        // =============================
        [Required]
        public Guid CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; } = default!;

        // =============================
        // Relationships: Program (Optional)
        // =============================
        public Guid? ProgramId { get; set; }

        [ForeignKey(nameof(ProgramId))]
        public virtual Programs? Program { get; set; }

        // =============================
        // Enrollment Info
        // =============================
        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        [Required, MaxLength(50)]
        public string Status { get; set; } = "Active";

        [Column(TypeName = "decimal(5,2)")]
        public decimal? Grade { get; set; }

        [MaxLength(255)]
        public string? StatusChangeReason { get; set; }

        // =============================
        // Navigation: Certificates
        // =============================
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

        // Required by EF Core proxy generation
        public Enrollment() { }
    }
}
