using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    public class Certificate
    {
        [Key]
        public int CertificateId { get; set; }

        // Foreign Keys
        [Required]
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))] // Navigation property to the associated course
                                       // public Course Course { get; set; } = default!;

        [Required]
        public int UserId { get; set; } // Foreign key to the user who earned the certificate
        [ForeignKey(nameof(UserId))] // Navigation property to the associated user
                                     // public DemoUser User { get; set; } = default!;


        [Required]
        [MaxLength(520)]
        public string CertificateUrl { get; set; } = default!; // URL to view/verify/download the certificate


        [Required]
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow; // Issue date/time (UTC recommended)

        // Fluent API configurations 
        //        mb.Entity<Certificate>()
        //                 .HasIndex(c => new { c.UserId, c.CourseId
        //    })
        //                 .IsUnique(); // ensures one certificate per user per course

        //    mb.Entity<Certificate>()
        //                 .HasIndex(c => c.CertificateUrl)
        //                 .IsUnique(); // ensures no duplicate certificate URLs
        //
    }
}
