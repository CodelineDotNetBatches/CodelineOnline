using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a certificate issued to a user for completing a course enrollment.
    /// Each certificate is linked to an enrollment (user + course combination).
    /// </summary>
    public class Certificate
    {
        [Key]
        public Guid CertificateId { get; set; }

        // =======================
        // Foreign Keys & Relations
        // =======================

        [Required]
        public Guid EnrollmentId { get; set; }

        // Marked as virtual to support proxy creation
        public virtual Enrollment Enrollment { get; set; } = default!;

        [Required]
        public Guid CourseId { get; set; }

        public virtual Course Course { get; set; } = default!;

        [Required]
        public Guid UserId { get; set; }

        // Uncomment later when User entity is ready
        // public virtual User User { get; set; } = default!;

        // =======================
        // Metadata
        // =======================

        [Required]
        [MaxLength(520)]
        public string CertificateUrl { get; set; } = default!;

        [Required]
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        // Protected constructor (required for EF proxies)
        protected Certificate() { }
    }
}