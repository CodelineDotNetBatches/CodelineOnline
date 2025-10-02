using UserManagement.Models;

namespace UserManagement.Repositories
{
    /// <summary>
    /// Repository interface for managing trainees.
    /// </summary>
    public interface ITraineeRepository
    {
        Task<IEnumerable<Trainee>> GetAllAsync();
        Task<Trainee?> GetByIdAsync(Guid id);
        Task AddAsync(Trainee trainee);
        Task UpdateAsync(Trainee trainee);
        Task DeleteAsync(Guid id);
    }
}
