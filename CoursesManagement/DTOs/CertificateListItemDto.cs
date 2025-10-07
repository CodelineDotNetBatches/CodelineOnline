namespace CoursesManagement.DTOs
{
    /// <summary>Compact list item for tables/search results.</summary>
    public class CertificateListItemDto
    {
        public Guid CertificateId { get; set; }
        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
        public string CertificateUrl { get; set; } = default!;
        public DateTime IssuedAt { get; set; }
    }
}
