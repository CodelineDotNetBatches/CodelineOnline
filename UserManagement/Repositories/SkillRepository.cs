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
    /// Provides CRUD operations plus trainee-skill linking logic.
    /// </summary>
    public class SkillRepository : GenericRepo<Skill>, ISkillRepository
    {
        private readonly UsersDbContext _db;

        public SkillRepository(UsersDbContext context) : base(context)
        {
            _db = context;
        }

        /// <summary>
        /// Gets all skills assigned to a specific trainee.
        /// </summary>
        public async Task<IEnumerable<Skill>> GetSkillsByTraineeAsync(Guid traineeId)
        {
            return await _db.TraineeSkills
                .Where(ts => ts.TraineeId == traineeId)
                .Select(ts => ts.Skill)
                .ToListAsync();
        }

        /// <summary>
        /// Assigns a skill to a trainee (creates a record in TraineeSkills).
        /// </summary>
        public async Task AssignSkillToTraineeAsync(Guid traineeId, int skillId)
        {
            var traineeSkill = new TraineeSkill
            {
                TraineeId = traineeId,
                SkillId = skillId
            };

            _db.TraineeSkills.Add(traineeSkill);
            await _db.SaveChangesAsync();
        }
    }
}
