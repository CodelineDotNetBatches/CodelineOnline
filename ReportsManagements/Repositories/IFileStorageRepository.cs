using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface IFileStorageRepository
    {
        Task AddAsync(FileStorage file);
        Task DeleteAsync(int id);
        Task<IEnumerable<FileStorage>> GetAllAsync();
        Task<FileStorage?> GetByIdAsync(int id);
        Task UpdateAsync(FileStorage file);
    }
}