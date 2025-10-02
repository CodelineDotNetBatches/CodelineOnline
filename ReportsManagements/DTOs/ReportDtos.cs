namespace ReportsManagements.DTOs
{
    // Trainer Report DTO
    public record TrainerReportUpsertDto(
    string TrainerId, string CourseId,
    int TotalSessions, int TotalStudents, double AttendanceRate);

    // Course Report DTO
    public record CourseReportUpsertDto(
        string CourseId, int TotalSessions, int TotalStudents, double AverageAttendanceRate);
}
