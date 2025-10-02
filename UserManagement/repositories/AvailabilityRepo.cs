using Microsoft.EntityFrameworkCore;
using System;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Repositories
{
    public class AvailabilityRepository : IAvailabilityRepository
    {
        private readonly UsersDbContext _db;
        public AvailabilityRepository(UsersDbContext db) => _db = db;

        public IQueryable<Availability> Query() => _db.Availabilities.AsQueryable();

        public Task<Availability?> GetAsync(int instructorId, int availabilityId, CancellationToken ct = default) =>
            _db.Availabilities.FirstOrDefaultAsync(a => a.InstructorId == instructorId && a.avilabilityId == availabilityId, ct);

        public async Task AddAsync(Availability entity, CancellationToken ct = default) =>
            await _db.Availabilities.AddAsync(entity, ct);

        public void Update(Availability entity) => _db.Availabilities.Update(entity);
        public void Remove(Availability entity) => _db.Availabilities.Remove(entity);

        public Task<int> SaveAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
        public int Save() => _db.SaveChanges();

        // simple per-instructor incremental availabilityId
        public async Task<int> NextAvailabilityIdAsync(int instructorId, CancellationToken ct = default)
        {
            var max = await _db.Availabilities
                .Where(a => a.InstructorId == instructorId)
                .Select(a => (int?)a.avilabilityId)
                .MaxAsync(ct) ?? 0;
            return max + 1;
        }
    }

}