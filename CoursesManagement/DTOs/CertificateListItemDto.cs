namespace CoursesManagement.DTOs
{
    /// <summary>Compact list item for tables/search results.</summary>
    public class CertificateListItemDto
    {
        public int CertificateId { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public string CertificateUrl { get; set; } = default!;
        public DateTime IssuedAt { get; set; }
    }
}
