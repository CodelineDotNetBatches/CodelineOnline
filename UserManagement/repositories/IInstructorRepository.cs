using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IInstructorRepository
    {
        IQueryable<Instructor> Query(); // for filters in service
        Task<Instructor?> GetAsync(int id, CancellationToken ct = default);
        Instructor? Get(int id);
        Task AddAsync(Instructor entity, CancellationToken ct = default);
        void Update(Instructor entity);
        void Remove(Instructor entity);
        Task<int> SaveAsync(CancellationToken ct = default);
        int Save();
    }
}