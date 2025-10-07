namespace ReportsManagements.DTOs
{
    public class BranchReportDtos
    {
        // DTO for creating a new BranchRepor
        public class BranchReportCreateDto
        {
            public int BranchId { get; set; }
            public int TotalSessions { get; set; }
            public int TotalStudents { get; set; }
            public int AttendanceRate { get; set; }
            public int TotalInstructors { get; set; }
        }
        // DTO for returning BranchReport data

        public class BranchReportResponseDto
        {
            public int BranchReportId { get; set; }
            public int BranchId { get; set; }
            public int TotalSessions { get; set; }
            public int TotalStudents { get; set; }
            public int AttendanceRate { get; set; }
            public int TotalInstructors { get; set; }
        }


    }
}
