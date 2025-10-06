using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface ITraineeRepository
    {
        Task<IEnumerable<Trainee>> GetAllAsync();
        Task<Trainee?> GetByIdAsync(int id);
        Task AddAsync(Trainee trainee);
        Task UpdateAsync(Trainee trainee);
        Task DeleteAsync(int id);

        // Custom methods
        Task<Trainee?> GetByIdWithSkillsAsync(int traineeId);
        Task SaveAsync(); //No parameter here
     
        
    }
}
