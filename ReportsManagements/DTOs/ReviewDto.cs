namespace ReportsManagements.DTOs
{
    public class ReviewDto
    {
        public string ReviewStatus { get; set; } = ""; // Pending, Approved, Rejected
        public string ReviewedBy { get; set; } = "";
    }
}
