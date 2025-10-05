using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto?> GetSkillByIdAsync(int id);
        Task AssignSkillToTraineeAsync(Guid traineeId, int skillId);
        Task RemoveSkillFromTraineeAsync(Guid traineeId, int skillId);
        
        Task AddSkillAsync(SkillDto dto);
        Task UpdateSkillAsync(SkillDto dto);
        Task DeleteSkillAsync(int id);
    }
}
