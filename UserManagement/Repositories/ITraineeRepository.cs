using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface ITraineeRepository
    {
        Task<IEnumerable<Trainee>> GetAllAsync();
        Task<Trainee?> GetByIdAsync(Guid id);
        Task AddAsync(Trainee trainee);
        Task UpdateAsync(Trainee trainee);
        Task DeleteAsync(Guid id);

        // Custom methods
        Task<Trainee?> GetByIdWithSkillsAsync(Guid traineeId);
        Task SaveAsync(); //No parameter here
     
        
    }
}
