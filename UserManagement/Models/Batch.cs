using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    /// <summary>
    /// Represents a training batch that groups multiple trainees.
    /// </summary>
    public class Batch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BatchId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Planned"; // e.g., Planned, Ongoing, Completed

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [MaxLength(256)]
        public string? Timeline { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        //  One-to-Many: One Batch → Many Trainees
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
    }
}
