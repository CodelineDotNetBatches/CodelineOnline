using UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
 // ✅ Add this

namespace UserManagement.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetSkillsByTraineeAsync(Guid traineeId);

        Task AssignSkillToTraineeAsync(Guid traineeId, int skillId);
    }
}
