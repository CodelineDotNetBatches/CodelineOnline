using UserManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
 // ✅ Add this

namespace UserManagement.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetSkillsByTraineeAsync(int traineeId);

        Task AssignSkillToTraineeAsync(int traineeId, int skillId);
    }
}
