namespace UserManagement.Models
{
    /// <summary>
    /// Represents a training batch.
    /// </summary>
    public class Batch
    {
        public Guid BatchId { get; set; }
        public string Name { get; set; } = default!;
        public string Status { get; set; } = "Planned";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Timeline { get; set; }
        public string? Description { get; set; }

        // Navigation: many-to-many with Users (Trainees)
        public ICollection<BatchTrainee> BatchTrainees { get; set; } = new List<BatchTrainee>();
    }

}
