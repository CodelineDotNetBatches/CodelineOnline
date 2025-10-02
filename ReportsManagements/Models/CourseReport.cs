using System.ComponentModel.DataAnnotations;

namespace ReportsManagements.Models
{
    public class CourseReport
    {
        [Key]
        public int CourseReportId { get; set; }
        public int TotalSessions { get; set; }
        public int TotalStudents { get; set; }
        public decimal AverageAttendanceRate { get; set; }
        public int CourseId { get; set; }
    }
}
