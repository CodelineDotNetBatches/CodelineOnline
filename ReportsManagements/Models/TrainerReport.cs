using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    public class TrainerReport
    {
        [Key]
        public int TrainerReportId { get; set; }
        public int TotalSessions { get; set; }
        public int TotalStudents { get; set; }
        public int AttendanceRate { get; set; }
        public int TrainerId { get; set; }
        public int CourseId { get; set; }
    }
}
