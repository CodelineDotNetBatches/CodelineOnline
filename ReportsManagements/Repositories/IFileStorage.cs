





namespace ReportsManagements.Repositories
{
    public interface IFileStorage
    {
        Task AddAsync(Models.FileStorage file);
        Task DeleteAsync(int id);
        Task<IEnumerable<Models.FileStorage>> GetAllAsync();
        Task<Models.FileStorage?> GetByIdAsync(int id);
        Task UpdateAsync(Models.FileStorage file);
    }
}