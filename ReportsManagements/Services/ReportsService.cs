using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.DTOs;

namespace ReportsManagements.Services;

public class ReportsService : IReportsService
{
    private readonly ITrainerReportRepository _trainerRepo;
    private readonly ICourseReportRepository _courseRepo;
    private readonly IMapper _mapper;

    public ReportsService(ITrainerReportRepository t, ICourseReportRepository c, IMapper m)
    { _trainerRepo = t; _courseRepo = c; _mapper = m; }

    public async Task<int> UpsertTrainerBatchAsync(IEnumerable<TrainerReportUpsertDto> items, CancellationToken ct = default)
    {
        var entities = items.Select(_mapper.Map<TrainerReport>).ToList();
        await _trainerRepo.UpsertBatchAsync(entities, ct);
        return entities.Count;
    }

    public async Task<int> UpsertCourseBatchAsync(IEnumerable<CourseReportUpsertDto> items, CancellationToken ct = default)
    {
        var entities = items.Select(_mapper.Map<CourseReport>).ToList();
        await _courseRepo.UpsertBatchAsync(entities, ct);
        return entities.Count;
    }

    public IEnumerable<TrainerReport> TopTrainers(int take = 10) =>
        _trainerRepo.Query().OrderByDescending(x => x.AttendanceRate)
                   .ThenBy(x => x.TrainerId).Take(take).ToList();

    public IEnumerable<CourseReport> TopCourses(int take = 10) =>
        _courseRepo.Query().OrderByDescending(x => x.AverageAttendanceRate)
                 .ThenBy(x => x.CourseId).Take(take).ToList();

    public IEnumerable<TrainerReport> QueryTrainers(PagingQuery q)
    {
        var data = _trainerRepo.Query();
        if (!string.IsNullOrWhiteSpace(q.TrainerId) && int.TryParse(q.TrainerId, out var trainerId))
            data = data.Where(x => x.TrainerId == trainerId);
        if (!string.IsNullOrWhiteSpace(q.CourseId) && int.TryParse(q.CourseId, out var courseId))
            data = data.Where(x => x.CourseId == courseId);

        data = (q.SortBy?.ToLowerInvariant()) switch
        {
            "sessions" => (q.Desc ? data.OrderByDescending(x => x.TotalSessions) : data.OrderBy(x => x.TotalSessions)),
            "students" => (q.Desc ? data.OrderByDescending(x => x.TotalStudents) : data.OrderBy(x => x.TotalStudents)),
            "rate" => (q.Desc ? data.OrderByDescending(x => x.AttendanceRate) : data.OrderBy(x => x.AttendanceRate)),
            _ => data.OrderBy(x => x.TrainerId)
        };
        return data.Skip((q.Page - 1) * q.PageSize).Take(q.PageSize).ToList();
    }

    public IEnumerable<CourseReport> QueryCourses(PagingQuery q)
    {
        var data = _courseRepo.Query();
        if (!string.IsNullOrWhiteSpace(q.CourseId) && int.TryParse(q.CourseId, out var courseId))
            data = data.Where(x => x.CourseId == courseId);

        data = (q.SortBy?.ToLowerInvariant()) switch
        {
            "sessions" => (q.Desc ? data.OrderByDescending(x => x.TotalSessions) : data.OrderBy(x => x.TotalSessions)),
            "students" => (q.Desc ? data.OrderByDescending(x => x.TotalStudents) : data.OrderBy(x => x.TotalStudents)),
            "rate" => (q.Desc ? data.OrderByDescending(x => x.AverageAttendanceRate) : data.OrderBy(x => x.AverageAttendanceRate)),
            _ => data.OrderBy(x => x.CourseId)
        };
        return data.Skip((q.Page - 1) * q.PageSize).Take(q.PageSize).ToList();
    }

    public IEnumerable<TrainerReport> AllTrainers() => _trainerRepo.Query().ToList();
    public IEnumerable<CourseReport> AllCourses() => _courseRepo.Query().ToList();

    public ReportOverviewDto Overview()
    {
        var trainers = _trainerRepo.Query();
        var courses = _courseRepo.Query();

        var totalTrainers = trainers.Select(t => t.TrainerId).Distinct().Count();
        var totalCourses = courses.Select(c => c.CourseId).Distinct().Count();

        // نحسب متوسط الحضور من بيانات المدربين إن وُجدت، وإلا من الكورسات
        var avgFromTrainers = trainers.Any()
            ? trainers.Average(t => (double)t.AttendanceRate)
            : (double?)null;

        var avgFromCourses = courses.Any()
            ? courses.Average(c => (double)c.AverageAttendanceRate)
            : (double?)null;

        double avg = avgFromTrainers ?? avgFromCourses ?? 0.0;

        return new ReportOverviewDto(
            TotalCourses: totalCourses,
            TotalTrainers: totalTrainers,
            AvgAttendanceRate: Math.Round(avg, 3),
            GeneratedAtUtc: DateTime.UtcNow
        );
    }



}

