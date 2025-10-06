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
    public class InstructorSkillRepository : GenericRepository<InstructorSkill>, IInstructorSkillRepository
    {
        private readonly UsersDbContext _db;

        public InstructorSkillRepository(UsersDbContext context) : base(context)
        {
            _db = context;
        }

        // ============================================================
        // 🔹 Common Skill Operations
        // ============================================================

        ///<summary>
        /// Add a new skill to the system.
        /// </summary>
        public async Task AddSkillAsync(InstructorSkill skill)
        {
            await _db.InstructorSkills.AddAsync(skill);
            await _db.SaveChangesAsync();
        }


        /// <summary>
        /// Retrieves all instructors skills in the system.
        /// </summary>
        public async Task<IEnumerable<InstructorSkill>> GetAllSkillsAsync()
        {
            return await _db.InstructorSkills.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves a skills by Instructor id .
        /// </summary>
        public async Task<InstructorSkill?> GetSkillByIdAsync(int Instructor_Id)
        {
            return await _db.InstructorSkills
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.InstructorId == Instructor_Id);
        }

        // ============================================================
        // 🔹 Trainee Skill Operations
        // ============================================================

        /// <summary>
        /// Gets all skills assigned to a specific trainee.
        /// </summary>
        //public async Task<IEnumerable<InstructorSkill>> GetSkillsByTraineeAsync(Guid userID)
        //{
        //    return await _db.Trainees
        //        .Where(t => t.TraineeId == userID)
        //        .SelectMany(t => t.traineeSkills)
        //        .AsNoTracking()
        //        .ToListAsync();
        //}

        /// <summary>
        /// Assigns a skill to a trainee.
        /// </summary>
        //public async Task AssignSkillToTraineeAsync(Guid userId, int skillId)
        //{
        //    var trainee = await _db.Trainees
        //        .Include(t => t.Skills)
        //        .FirstOrDefaultAsync(t => t.TraineeId == userId);

        //    var skill = await _db.Skills.FindAsync(skillId);

        //    if (trainee == null || skill == null)
        //        throw new Exception("Invalid trainee or skill.");

        //    trainee.Skills ??= new List<Skill>();

        //    if (!trainee.Skills.Any(s => s.SkillId == skillId))
        //    {
        //        trainee.Skills.Add(skill);
        //        await _db.SaveChangesAsync();
        //    }
        //}

        // ============================================================
        // 🔹 Instructor Skill Operations
        // ============================================================

        /// <summary>
        /// Gets all skills assigned to a specific instructor.
        /// </summary>
        public async Task<IEnumerable<InstructorSkill>> GetSkillsByInstructorAsync(int instructorID)
        {
            return await _db.Instructors
                .Where(i => i.InstructorId == instructorID)
                .SelectMany(i => i.instructorSkills)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Assigns a skill to an instructor.
        /// </summary>
        public async Task AssignSkillToInstructorAsync(int instructorID, int skillId)
        {
            var instructor = await _db.Instructors
                .Include(i => i.instructorSkills)
                .FirstOrDefaultAsync(i => i.InstructorId == instructorID);

            var skill = await _db.InstructorSkills.FindAsync(skillId);

            if (instructor == null || skill == null)
                throw new Exception("Invalid instructor or skill.");

            instructor.instructorSkills ??= new List<InstructorSkill>();

            if (!instructor.instructorSkills.Any(s => s.InstructorSkillId == skillId))
            {
                instructor.instructorSkills.Add(skill);
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Removes a skill from an instructor.
        /// </summary>
        public async Task RemoveSkillFromInstructorAsync(int InstructorID, int skillId)
        {
            var instructor = await _db.Instructors
                .Include(i => i.instructorSkills)
                .FirstOrDefaultAsync(i => i.InstructorId == InstructorID);

            if (instructor == null)
                throw new Exception($"Instructor {InstructorID} not found.");

            var skill = instructor.instructorSkills.FirstOrDefault(s => s.InstructorSkillId == skillId);
            if (skill == null)
                throw new Exception($"Skill {skillId} not found for this instructor.");

            instructor.instructorSkills.Remove(skill);
            await _db.SaveChangesAsync();
        }

        /// <summary>   
        /// update a skill
        /// <summary>

        public void UpdateSkill(InstructorSkill skill)
        {
            _db.InstructorSkills.Update(skill);
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
