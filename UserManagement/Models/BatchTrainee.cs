namespace UserManagement.Models
{
    /// <summary>
    /// Join table for many-to-many relationship between Batch and Trainee.
    /// </summary>
    public class BatchTrainee
    {
        public Guid BatchId { get; set; }
        public Batch Batch { get; set; } = default!;

        public Guid TraineeId { get; set; }
        public Trainee Trainee { get; set; } = default!;
    }
}
