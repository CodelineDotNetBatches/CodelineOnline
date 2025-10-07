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

    
    [HttpGet("overview")]
    public IActionResult Overview() => Ok(_svc.Overview());

    
    [HttpGet("trainer/export")]
    public IActionResult ExportTrainers([FromQuery] int? trainerId, [FromQuery] int? courseId,
                                        [FromQuery] string? sortBy, [FromQuery] bool desc = false)
    {
        var data = _svc.AllTrainers().AsQueryable();
        if (trainerId.HasValue) data = data.Where(x => x.TrainerId == trainerId.Value);
        if (courseId.HasValue) data = data.Where(x => x.CourseId == courseId.Value);

        data = (sortBy?.ToLowerInvariant()) switch
        {
            "sessions" => desc ? data.OrderByDescending(x => x.TotalSessions) : data.OrderBy(x => x.TotalSessions),
            "students" => desc ? data.OrderByDescending(x => x.TotalStudents) : data.OrderBy(x => x.TotalStudents),
            "rate" => desc ? data.OrderByDescending(x => x.AttendanceRate) : data.OrderBy(x => x.AttendanceRate),
            _ => data.OrderBy(x => x.TrainerId)
        };

        var sb = new StringBuilder();
        sb.AppendLine("TrainerId,CourseId,TotalSessions,TotalStudents,AttendanceRate");
        foreach (var r in data)
            sb.AppendLine($"{r.TrainerId},{r.CourseId},{r.TotalSessions},{r.TotalStudents},{r.AttendanceRate}");

        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "trainer_reports.csv");
    }

    
    [HttpGet("course/export")]
    public IActionResult ExportCourses([FromQuery] int? courseId,
                                       [FromQuery] string? sortBy, [FromQuery] bool desc = false)
    {
        var data = _svc.AllCourses().AsQueryable();
        if (courseId.HasValue) data = data.Where(x => x.CourseId == courseId.Value);

        data = (sortBy?.ToLowerInvariant()) switch
        {
            "sessions" => desc ? data.OrderByDescending(x => x.TotalSessions) : data.OrderBy(x => x.TotalSessions),
            "students" => desc ? data.OrderByDescending(x => x.TotalStudents) : data.OrderBy(x => x.TotalStudents),
            "rate" => desc ? data.OrderByDescending(x => x.AverageAttendanceRate) : data.OrderBy(x => x.AverageAttendanceRate),
            _ => data.OrderBy(x => x.CourseId)
        };

        var sb = new StringBuilder();
        sb.AppendLine("CourseId,TotalSessions,TotalStudents,AverageAttendanceRate");
        foreach (var r in data)
            sb.AppendLine($"{r.CourseId},{r.TotalSessions},{r.TotalStudents},{r.AverageAttendanceRate}");

        return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "course_reports.csv");
    }
}
