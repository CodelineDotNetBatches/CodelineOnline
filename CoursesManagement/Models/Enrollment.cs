using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a student's enrollment in a course (optionally under a program).
    /// </summary>
    [Index(nameof(UserId), nameof(CourseId), nameof(ProgramId), IsUnique = true, Name = "IX_Enrollment_User_Course_Program")]
    public class Enrollment
    {
        /// <summary>
        /// Unique identifier for the enrollment record.
        /// </summary>
        [Key]
        public Guid EnrollmentId { get; set; }

        /// <summary>
        /// Date when the enrollment was created.
        /// </summary>
        [Required]
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Foreign key to the enrolled user (student/trainee).
        /// </summary>
        [Required]
        public Guid UserId { get; set; }

        /// <summary>
        /// Reference navigation property to the enrolled user.
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = default!;

        /// <summary>
        /// Foreign key to the course.
        /// </summary>
        [Required]
        public Guid CourseId { get; set; }

        /// <summary>
        /// Reference navigation property to the enrolled course.
        /// </summary>
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = default!;

        /// <summary>
        /// Optional foreign key to the program (if enrollment is tracked under a program).
        /// </summary>
        public Guid? ProgramId { get; set; }

        /// <summary>
        /// Reference navigation property to the program.
        /// </summary>
        [ForeignKey(nameof(ProgramId))]
        public Programs? Program { get; set; }

        /// <summary>
        /// Current enrollment status (Active, Completed, Dropped, etc.).
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Active";
    }
}
