namespace ReportsManagements.Models
{
    public class ErrorResponse
    {
            public int Status { get; set; }
            public string Code { get; set; } = string.Empty; // e.g., "BadRequest", "NotFound"
        public string Message { get; set; } = string.Empty; // Human-readable message
        public object? Details { get; set; } // Optional additional details
    }
}
