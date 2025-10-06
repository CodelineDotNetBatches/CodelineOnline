


namespace ReportsManagements.Repositories
{
    // Interface defining CRUD operations for ReasonCode
    public interface IReasonCodeRepository
    {
        Task AddAsync(Models.ReasonCode reasonCode);
        Task DeleteAsync(int id);
        Task<IEnumerable<Models.ReasonCode>> GetAllAsync();
        Task<Models.ReasonCode> GetByIdAsync(int id);
        Task UpdateAsync(Models.ReasonCode reasonCode);
    }
}