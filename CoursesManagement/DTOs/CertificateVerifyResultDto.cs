namespace CoursesManagement.DTOs
{
    /// <summary>Response when verifying a certificate by URL or User+Course.</summary>
    public class CertificateVerifyResultDto
    {
        public bool Found { get; set; }
        public int? CertificateId { get; set; }
        public int? CourseId { get; set; }
        public int? UserId { get; set; }
        public string? CertificateUrl { get; set; }
        public DateTime? IssuedAt { get; set; }
        public string? Message { get; set; } // "Valid", "Not found", etc.
    }
}

