using Microsoft.EntityFrameworkCore;
using System;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly UsersDbContext _db;
        public InstructorRepository(UsersDbContext db) => _db = db;

        public IQueryable<Instructor> Query() => _db.Instructors.AsQueryable();

        public Task<Instructor?> GetAsync(int id, CancellationToken ct = default) =>
            _db.Instructors.Include(i => i.Availabilities).FirstOrDefaultAsync(i => i.InstructorId == id, ct);

        public Instructor? Get(int id) =>
            _db.Instructors.Include(i => i.Availabilities).FirstOrDefault(i => i.InstructorId == id);

        public async Task AddAsync(Instructor entity, CancellationToken ct = default) =>
            await _db.Instructors.AddAsync(entity, ct);

        public void Update(Instructor entity) => _db.Instructors.Update(entity);
        public void Remove(Instructor entity) => _db.Instructors.Remove(entity);

        public Task<int> SaveAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
        public int Save() => _db.SaveChanges();
    }

}