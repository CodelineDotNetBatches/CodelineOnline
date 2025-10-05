namespace CoursesManagement.DTOs
{
    /// <summary>Detailed view for a single certificate.</summary>
    public class CertificateDetailsDto
    {
        public int CertificateId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string CertificateUrl { get; set; } = default!;
        public DateTime IssuedAt { get; set; }

        // Optionally extend with related info later:
        public string? CourseName { get; set; }
        public string? UserName { get; set; }
        public DateTime IssuedDate => IssuedAt;
    }
}

