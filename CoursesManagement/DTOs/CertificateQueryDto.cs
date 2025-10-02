namespace CoursesManagement.DTOs
{
    /// <summary>Filter for listing/searching certificates.</summary>
    public class CertificateQueryDto
    {
        public int? UserId { get; set; }
        public int? CourseId { get; set; }
        public DateTime? IssuedFrom { get; set; }
        public DateTime? IssuedTo { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string? SortBy { get; set; } // e.g., "issuedAt"
        public bool Desc { get; set; } = true;
    }
}

