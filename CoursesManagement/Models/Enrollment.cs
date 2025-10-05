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
        /// <summary>
        /// Unique identifier for the enrollment.
        /// </summary>
        [Key]
        public Guid EnrollmentId { get; set; }

        // =============================
        // Relationships: User (Student)
        // =============================
        /// <summary>
        /// Foreign key to the enrolled user (student).
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property to the enrolled user.
        /// </summary>
        public User User { get; set; } = default!;

        // =============================
        // Relationships: Course
        // =============================
        /// <summary>
        /// Foreign key to the enrolled course.
        /// </summary>
        [Required]
        public Guid CourseId { get; set; }

        /// <summary>
        /// Navigation property to the enrolled course.
        /// </summary>
        public Course Course { get; set; } = default!;

        // =============================
        // Relationships: Program (Optional)
        // =============================
        /// <summary>
        /// Optional foreign key to the program under which the enrollment is made.
        /// </summary>
        public Guid? ProgramId { get; set; }

        /// <summary>
        /// Navigation property to the program.
        /// </summary>
        public Programs? Program { get; set; }

        // =============================
        // Enrollment Info
        // =============================
        /// <summary>
        /// Date and time when the user enrolled.
        /// </summary>
        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Current enrollment status (Active, Completed, Dropped).
        /// </summary>
        [Required, MaxLength(50)]
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Optional grade achieved upon completion.
        /// </summary>
        [Column(TypeName = "decimal(5,2)")]
        public decimal? Grade { get; set; }

        /// <summary>
        /// Reason for changing status (e.g., "Dropped due to absence").
        /// </summary>
        [MaxLength(255)]
        public string? StatusChangeReason { get; set; }
    }
}
