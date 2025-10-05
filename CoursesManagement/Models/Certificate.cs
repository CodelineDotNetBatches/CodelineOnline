using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{ 
  //Represents a certificate issued to a user for completing a course enrollment.
    public class Certificate
    {
        
        
        [Key]
        public Guid CertificateId { get; set; } // Primary key for the certificate.


        // Foreign key to the enrollment that generated this certificate.
        // Each certificate belongs to one enrollment (user + course).
        [Required]
        public Guid EnrollmentId { get; set; }

    
        
        public Enrollment Enrollment { get; set; } = default!; // Navigation property to the associated enrollment.


        
        [Required]
        public Guid CourseId { get; set; } // Foreign key to the course for which the certificate was issued.


        
        public Course Course { get; set; } = default!; // Navigation property to the related course.


        
        [Required]
        public Guid UserId { get; set; } // Foreign key to the user who earned the certificate.


        
      //  public User User { get; set; } = default!; // Navigation property to the related user.


        // URL pointing to the hosted or downloadable version of the certificate.
        [Required]
        [MaxLength(520)]
        public string CertificateUrl { get; set; } = default!;


        // The UTC date and time the certificate was issued.
        [Required]
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    }
}
