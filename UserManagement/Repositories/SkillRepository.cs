using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Repositories
{
    public class SkillRepository : GenericRepository<Skill>, ISkillRepository
    {
        private readonly UsersDbContext _db; // optional, if you want direct access

        public SkillRepository(UsersDbContext context) : base(context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Skill>> GetSkillsByTraineeAsync(Guid traineeId)
        {
            return await _db.TraineeSkills
                .Where(ts => ts.TraineeId == traineeId)
                .Select(ts => ts.Skill)
                .ToListAsync();
        }

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
