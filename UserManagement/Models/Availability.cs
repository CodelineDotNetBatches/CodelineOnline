using System.ComponentModel.DataAnnotations.Schema;
namespace UserManagement.Models
{
    public class Availability
    {
        // Partial key with PK for for this weak entity
        
        public int avilabilityId { get; set; } // availabilityId with instructorId will be as composite key for this entity 
        // Foreign Key to Instructor
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        // Navigation property
        public Instructor Instructor { get; set; }

        public string Avail_Status { get; set; } // Active/ Inactive /Complete/Busy

        public string day_of_week { get; set; } // Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday

        public DateTime time { get; set; } // 12:00 AM/PM
    }
}
