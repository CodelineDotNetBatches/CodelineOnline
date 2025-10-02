using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Services;
using System.Text;

namespace ReportsManagements.Controllers;

[ApiController]
[Route("api/v1/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportsService _svc;
    public ReportsController(IReportsService svc) => _svc = svc;

    [HttpPost("trainer/rebuild")]
    public async Task<IActionResult> RebuildTrainer([FromBody] IEnumerable<TrainerReportUpsertDto> body, CancellationToken ct)
        => Ok(new { upserts = await _svc.UpsertTrainerBatchAsync(body, ct) });

    [HttpPost("course/rebuild")]
    public async Task<IActionResult> RebuildCourse([FromBody] IEnumerable<CourseReportUpsertDto> body, CancellationToken ct)
        => Ok(new { upserts = await _svc.UpsertCourseBatchAsync(body, ct) });

    [HttpGet("trainer/top")]
    public IActionResult TopTrainers([FromQuery] int take = 10) => Ok(_svc.TopTrainers(take));

    [HttpGet("course/top")]
    public IActionResult TopCourses([FromQuery] int take = 10) => Ok(_svc.TopCourses(take));

    [HttpGet("trainer")]
    public IActionResult QueryTrainers([FromQuery] PagingQuery q) => Ok(_svc.QueryTrainers(q));

    [HttpGet("course")]
    public IActionResult QueryCourses([FromQuery] PagingQuery q) => Ok(_svc.QueryCourses(q));

    [HttpGet("trainer/export")]
    public IActionResult ExportTrainers()
    {
        var sb = new StringBuilder();
        sb.AppendLine("TrainerId,CourseId,TotalSessions,TotalStudents,AttendanceRate");
        foreach (var r in _svc.AllTrainers())
            sb.AppendLine($"{r.TrainerId},{r.CourseId},{r.TotalSessions},{r.TotalStudents},{r.AttendanceRate}");
        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "trainer_reports.csv");
    }

    [HttpGet("course/export")]
    public IActionResult ExportCourses()
    {
        var sb = new StringBuilder();
        sb.AppendLine("CourseId,TotalSessions,TotalStudents,AverageAttendanceRate");
        foreach (var r in _svc.AllCourses())
            sb.AppendLine($"{r.CourseId},{r.TotalSessions},{r.TotalStudents},{r.AverageAttendanceRate}");
        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "course_reports.csv");
    }
}
