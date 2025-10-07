namespace ReportsManagements.DTOs
{
    public class TrainerReportDtos
    {
        public class TrainerReportCreateDto
        {
            public int TotalSessions { get; set; }
            public int TotalStudents { get; set; }
            public decimal AttendanceRate { get; set; }
            public int TrainerId { get; set; }
            public int CourseId { get; set; }
        }

        public class TrainerReportUpdateDto
        {
            public int TrainerReportId { get; set; }
            public int TotalSessions { get; set; }
            public int TotalStudents { get; set; }
            public decimal AttendanceRate { get; set; }
            public int TrainerId { get; set; }
            public int CourseId { get; set; }
        }
        public class TrainerReportResponseDto
        {
            public int TrainerReportId { get; set; }
            public int TotalSessions { get; set; }
            public int TotalStudents { get; set; }
            public decimal AttendanceRate { get; set; }
            public int TrainerId { get; set; }
            public int CourseId { get; set; }
        }
    }
}
