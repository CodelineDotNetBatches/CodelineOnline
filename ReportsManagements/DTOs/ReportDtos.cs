namespace ReportsManagements.DTOs
{
    // Trainer Report DTO
    public record TrainerReportUpsertDto(
    string TrainerId, string CourseId,
    int TotalSessions, int TotalStudents, double AttendanceRate);

    // Course Report DTO
    public record CourseReportUpsertDto(
       
 
      string CourseId, int TotalSessions, int TotalStudents, double AverageAttendanceRate);

    public record PagingQuery(int Page = 1, int PageSize = 20,
                          string? SortBy = null, bool Desc = false,
                          string? TrainerId = null, string? CourseId = null);
}

