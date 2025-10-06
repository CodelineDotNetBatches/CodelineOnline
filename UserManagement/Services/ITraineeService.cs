using UserManagement.DTOs;

namespace UserManagement.Services
{
    /// <summary>
    /// Service interface for managing trainee operations.
    /// </summary>
    public interface ITraineeService
    {
        Task<IEnumerable<TraineeDTO>> GetAllTraineesAsync();
        Task<TraineeDTO?> GetTraineeByIdAsync(int id);
        Task<TraineeDTO> CreateTraineeAsync(TraineeDTO dto);
        Task<TraineeDTO> UpdateTraineeAsync(TraineeDTO dto);
        Task DeleteTraineeAsync(int id);
    }
}
