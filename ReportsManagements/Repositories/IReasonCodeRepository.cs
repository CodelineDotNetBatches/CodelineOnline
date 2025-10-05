using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface IReasonCodeRepository
    {
        Task AddAsync(ReasonCode reasonCode);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReasonCode>> GetAllAsync();
        Task<ReasonCode> GetByIdAsync(int id);
        Task UpdateAsync(ReasonCode reasonCode);
    }
}