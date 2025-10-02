using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IAvailabilityRepository
    {
        IQueryable<Availability> Query();
        Task<Availability?> GetAsync(int instructorId, int availabilityId, CancellationToken ct = default);
        Task AddAsync(Availability entity, CancellationToken ct = default);
        void Update(Availability entity);
        void Remove(Availability entity);
        Task<int> SaveAsync(CancellationToken ct = default);
        int Save();
        Task<int> NextAvailabilityIdAsync(int instructorId, CancellationToken ct = default);
    }
}