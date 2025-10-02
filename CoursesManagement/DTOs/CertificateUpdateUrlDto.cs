namespace CoursesManagement.DTOs
{
    /// <summary>Update an existing certificate’s URL (e.g., if re-hosted).</summary>
    public class CertificateUpdateUrlDto
    {
        public string CertificateUrl { get; set; } = default!;
    }
}

