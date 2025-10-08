using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace UserManagement.Models
{
    public class Availability
    {
        [Required]
        public int avilabilityId { get; set; } // spelled as provided; consider renaming -> AvailabilityId

        // Foreign Key to Instructor
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

        [Required, MaxLength(20)]
        public AvailabilityStatus Avail_Status { get; set; }   // Active/Inactive/Complete/Busy

        [Required, MaxLength(10)]
        public DaysOfWeek day_of_week { get; set; }  // Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
        
        [DataType(DataType.Time)]
        public TimeOnly time { get; set; } // 12:00 AM/PM

        // navs
        public virtual Instructor? Instructor { get; set; }

    }
    public enum AvailabilityStatus 
    { 
        Active = 1, 
        Inactive = 2, 
        Busy = 3, 
        Completed = 4 
    }
    public enum DaysOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }


}
