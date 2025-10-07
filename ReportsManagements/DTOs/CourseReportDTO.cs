namespace ReportsManagements.DTOs
{
    public class CourseReportCreateDto
    {
        public int TotalSessions { get; set; }
        public int TotalStudents { get; set; }
        public decimal AverageAttendanceRate { get; set; }
        public int CourseId { get; set; }
    }

    public class CourseReportResponseDto
    {
        public int CourseReportId { get; set; }
        public int TotalSessions { get; set; }
        public int TotalStudents { get; set; }
        public decimal AverageAttendanceRate { get; set; }
        public int CourseId { get; set; }
    }

    public class CourseReportUpdateDto
    {
        public int TotalSessions { get; set; }
        public int TotalStudents { get; set; }
        public decimal AverageAttendanceRate { get; set; }
    }
}
