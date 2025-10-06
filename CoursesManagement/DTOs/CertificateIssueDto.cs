namespace CoursesManagement.DTOs
{
    /// <summary>Issue a new certificate to a user for a specific course.</summary>
    public class CertificateIssueDto
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string CertificateUrl { get; set; } = default!;
        // Optional: allow custom issued date if needed
        public DateTime? IssuedAt { get; set; }
    }
}
