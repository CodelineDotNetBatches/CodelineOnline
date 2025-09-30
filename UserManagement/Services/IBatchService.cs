using UserManagement.DTOs;

namespace UserManagement.Services
{
    /// <summary>
    /// Contract for batch business logic.
    /// </summary>
    public interface IBatchService
    {
        Task<IEnumerable<BatchDTO>> GetAllBatchesAsync();
        Task<BatchDTO?> GetBatchByIdAsync(Guid id);
        Task<BatchDTO> CreateBatchAsync(BatchDTO dto);
        Task<BatchDTO> UpdateBatchAsync(BatchDTO dto);
        Task DeleteBatchAsync(Guid id);
    }
}
