namespace CoursesManagement.DTOs
{
    /// <summary>Detailed view for a single certificate.</summary>
    public class CertificateDetailsDto
    {
        public Guid CertificateId { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public string CertificateUrl { get; set; } = default!;
        public DateTime IssuedAt { get; set; }

        // Optionally extend with related info later:
        // public string? CourseTitle { get; set; }
        // public string? UserFullName { get; set; }
    }
}

