using UserManagement.Models;

namespace UserManagement.Repositories
{
    // <summary>
    /// Contract for batch data access.
    /// </summary>
    public interface IBatchRepository
    {
        Task<IEnumerable<Batch>> GetAllAsync();
        Task<Batch?> GetByIdAsync(Guid id);
        Task AddAsync(Batch batch);
        Task UpdateAsync(Batch batch);
        Task DeleteAsync(Guid id);
    }
}
