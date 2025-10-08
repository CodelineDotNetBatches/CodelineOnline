using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IAvailabilityRepository
    {
        Task AddAsync(Availability entity);
        Task<Availability?> GetAsync(int instructorId);
        Task<int> NextAvailabilityIdAsync(int instructorId);
        IQueryable<Availability> Query();
        void Remove(Availability entity);
        int Save();
        Task<int> SaveAsync();
        void Update(Availability entity);
    }
}