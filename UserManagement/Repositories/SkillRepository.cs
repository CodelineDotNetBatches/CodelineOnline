using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    /// <summary>
    /// Repository implementation for Skill entity.
    /// Provides CRUD operations and trainee/instructor skill linking logic.
    /// </summary>
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        private readonly UsersDbContext _db;

        public SkillRepository(UsersDbContext context) : base(context)
        {
            _db = context;
        }

        // ============================================================
        // 🔹 Common Skill Operations
        // ============================================================

        /// <summary>
        /// Retrieves all skills in the system.
        /// </summary>
        public async Task<IEnumerable<Skill>> GetAllSkillsAsync()
        {
            return await _db.Skills.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves a skill by its ID.
        /// </summary>
        public async Task<Skill?> GetSkillByIdAsync(int skillId)
        {
            return await _db.Skills
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SkillId == skillId);
        }

        // ============================================================
        // 🔹 Trainee Skill Operations
        // ============================================================

        /// <summary>
        /// Gets all skills assigned to a specific trainee.
        /// </summary>
        public async Task<IEnumerable<Skill>> GetSkillsByTraineeAsync(Guid userID)
        {
            return await _db.Trainees
                .Where(t => t.TraineeId == userID)
                .SelectMany(t => t.Skills)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Assigns a skill to a trainee.
        /// </summary>
        public async Task AssignSkillToTraineeAsync(Guid userId, int skillId)
        {
            var trainee = await _db.Trainees
                .Include(t => t.Skills)
                .FirstOrDefaultAsync(t => t.TraineeId == userId);

            var skill = await _db.Skills.FindAsync(skillId);

            if (trainee == null || skill == null)
                throw new Exception("Invalid trainee or skill.");

            trainee.Skills ??= new List<Skill>();

            if (!trainee.Skills.Any(s => s.SkillId == skillId))
            {
                trainee.Skills.Add(skill);
                await _db.SaveChangesAsync();
            }
        }

        // ============================================================
        // 🔹 Instructor Skill Operations
        // ============================================================

        /// <summary>
        /// Gets all skills assigned to a specific instructor.
        /// </summary>
        public async Task<IEnumerable<Skill>> GetSkillsByInstructorAsync(int userId)
        {
            return await _db.Instructors
                .Where(i => i.InstructorId == userId)
                .SelectMany(i => i.Skills)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Assigns a skill to an instructor.
        /// </summary>
        public async Task AssignSkillToInstructorAsync(int userId, int skillId)
        {
            var instructor = await _db.Instructors
                .Include(i => i.Skills)
                .FirstOrDefaultAsync(i => i.InstructorId == userId);

            var skill = await _db.Skills.FindAsync(skillId);

            if (instructor == null || skill == null)
                throw new Exception("Invalid instructor or skill.");

            instructor.Skills ??= new List<Skill>();

            if (!instructor.Skills.Any(s => s.SkillId == skillId))
            {
                instructor.Skills.Add(skill);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Removes a skill from an instructor.
        /// </summary>
        public async Task RemoveSkillFromInstructorAsync(int userId, int skillId)
        {
            var instructor = await _db.Instructors
                .Include(i => i.Skills)
                .FirstOrDefaultAsync(i => i.InstructorId == userId);

            if (instructor == null)
                throw new Exception($"Instructor {userId} not found.");

            var skill = instructor.Skills.FirstOrDefault(s => s.SkillId == skillId);
            if (skill == null)
                throw new Exception($"Skill {skillId} not found for this instructor.");

            instructor.Skills.Remove(skill);
            await _db.SaveChangesAsync();
        }

        // ============================================================
        // 🔹 Utility
        // ============================================================

        /// <summary>
        /// Saves pending database changes asynchronously.
        /// </summary>
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
