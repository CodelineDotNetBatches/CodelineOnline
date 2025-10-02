using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface ITrainerReportRepository
    {
        Task<TrainerReport> AddAsync(TrainerReport trainerReport);
        Task DeleteAsync(int id);
        Task<IEnumerable<TrainerReport>> GetAllAsync();
        Task<TrainerReport?> GetByIdAsync(int id);
        Task<TrainerReport?> UpdateAsync(TrainerReport trainerReport);
        Task UpsertBatchAsync(IEnumerable<TrainerReport> items, CancellationToken ct = default);
        IQueryable<TrainerReport> Query();

    }
}