using System.ComponentModel.DataAnnotations;
using UserManagement.Models;

namespace UserManagement.DTOs
{
    public class AvailabilityCreateDto
    {
        [Required] public int InstructorId { get; set; }
        public int? BatchId { get; set; }
        [Required] public AvailabilityStatus Avail_Status { get; set; }
        [Required] public DaysOfWeek Day_Of_Week { get; set; }
        [Required] public TimeOnly Time { get; set; }
    }

    public class AvailabilityUpdateDto : AvailabilityCreateDto { }

    public class AvailabilityReadDto
    {
        public int AvailablityId { get; set; }
        public int InstructorId { get; set; }
        //public int? BatchId { get; set; }
        public AvailabilityStatus Avail_Status { get; set; }
        public DaysOfWeek Day_Of_Week { get; set; }
        public TimeOnly Time { get; set; }
    }
}
