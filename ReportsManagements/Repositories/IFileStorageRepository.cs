





namespace ReportsManagements.Repositories
{
    // Interface defining CRUD operations for file storage
    public interface IFileStorageRepository
    {
        Task AddAsync(Models.FileStorage file);
        Task DeleteAsync(int id);
        Task<IEnumerable<Models.FileStorage>> GetAllAsync();
        Task<Models.FileStorage?> GetByIdAsync(int id);
        Task UpdateAsync(Models.FileStorage file);
    }
}