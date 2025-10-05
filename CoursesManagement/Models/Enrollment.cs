using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a student's enrollment in a course.
    /// </summary>
    [Index(nameof(UserId), nameof(CourseId), IsUnique = true, Name = "IX_Enrollment_UserId_CourseId")]
    public class Enrollment
    {
        /// <summary>
        /// Unique identifier for the enrollment record.
        /// </summary>
        [Key]
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// Foreign key to the enrolled user (student).
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Navigation property for the enrolled user.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = default!;

        /// <summary>
        /// Foreign key to the course being enrolled in.
        /// </summary>
        [Required]
        public Guid CourseId { get; set; }

        /// <summary>
        /// Navigation property for the course.
        /// </summary>
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; } = default!;

        /// <summary>
        /// Optional foreign key to the program (if enrollment is part of a program).
        /// </summary>
        public Guid? ProgramId { get; set; }

        /// <summary>
        /// Navigation property for the program.
        /// </summary>
        [ForeignKey(nameof(ProgramId))]
        public virtual Programs? Program { get; set; }

        /// <summary>
        /// Date and time when the enrollment was created.
        /// </summary>
        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Current enrollment status (e.g., Active, Completed, Dropped).
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Optional grade achieved in the course (0-100).
        /// </summary>
        [Range(0, 100)]
        public decimal? Grade { get; set; }

        /// <summary>
        /// Optional reason for the most recent status change.
        /// (e.g., "Dropped due to non-payment", "Completed with certificate").
        /// </summary>
        [StringLength(250)]
        public string? StatusChangeReason { get; set; }
    }
}
