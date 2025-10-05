using ReportsManagements.DTOs;
using ReportsManagements.Models;

namespace ReportsManagements.Services;
public interface IReportsService
{
    Task<int> UpsertTrainerBatchAsync(IEnumerable<TrainerReportUpsertDto> items, CancellationToken ct = default);
    Task<int> UpsertCourseBatchAsync(IEnumerable<CourseReportUpsertDto> items, CancellationToken ct = default);

    IEnumerable<TrainerReport> TopTrainers(int take = 10);
    IEnumerable<CourseReport> TopCourses(int take = 10);

    IEnumerable<TrainerReport> QueryTrainers(PagingQuery q);
    IEnumerable<CourseReport> QueryCourses(PagingQuery q);
    ReportOverviewDto Overview();
    IEnumerable<TrainerReport> AllTrainers();
    IEnumerable<CourseReport> AllCourses();
}
