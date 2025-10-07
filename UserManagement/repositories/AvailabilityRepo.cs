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

        public Task<Availability?> GetAsync(int instructorId) =>
            _db.Availabilities.FirstOrDefaultAsync(a => a.InstructorId == instructorId);

        public async Task AddAsync(Availability entity) =>
            await _db.Availabilities.AddAsync(entity);

        public void Update(Availability entity) => _db.Availabilities.Update(entity);
        public void Remove(Availability entity) => _db.Availabilities.Remove(entity);

        public Task<int> SaveAsync() => _db.SaveChangesAsync();
        public int Save() => _db.SaveChanges();

        public async Task<int> NextAvailabilityIdAsync(int instructorId)
        {
            // Get the current highest AvailabilityId for this instructor
            var maxId = await _db.Availabilities
                .Where(a => a.InstructorId == instructorId)
                .Select(a => (int?)a.avilabilityId)   // nullable to handle empty case
                .MaxAsync();

            // If no records exist, start from 1
            return (maxId ?? 0) + 1;
        }
    }

}