using UserManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Repositories
{
    /// <summary>
    /// Repository for trainee persistence and skill relations.
    /// </summary>
    public class TraineeRepository : ITraineeRepository
    {
        private readonly UsersDbContext _context;

        public TraineeRepository(UsersDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // BASIC CRUD
        // =========================================================

        public async Task<IEnumerable<Trainee>> GetAllAsync()
        {
            return await _context.Trainees
                .Include(t => t.traineeSkills) // Include related Skills
                .ToListAsync();
        }

        public async Task<Trainee?> GetByIdAsync(int id)
        {
            return await _context.Trainees.FindAsync(id);
        }

        public async Task AddAsync(Trainee trainee)
        {
            await _context.Trainees.AddAsync(trainee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Trainee trainee)
        {
            _context.Trainees.Update(trainee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee != null)
            {
                _context.Trainees.Remove(trainee);
                await _context.SaveChangesAsync();
            }
        }

        // =========================================================
        // CUSTOM METHODS
        // =========================================================

        /// <summary>
        /// Loads a trainee by ID including related skills (for assignment logic).
        /// </summary>
        public async Task<Trainee?> GetByIdWithSkillsAsync(int traineeId)
        {
            return await _context.Trainees
                .Include(t => t.traineeSkills)
                .FirstOrDefaultAsync(t => t.TraineeId == traineeId);
        }

        /// <summary>
        /// Saves pending changes asynchronously (used by service layer).
        /// </summary>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
