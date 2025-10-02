namespace UserManagement.DTOs
{
    /// <summary>
    /// DTO for transferring batch data.
    /// </summary>
    public class BatchDTO
    {
        public Guid BatchId { get; set; }
        public string Name { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Timeline { get; set; }
        public string? Description { get; set; }
    }
}
