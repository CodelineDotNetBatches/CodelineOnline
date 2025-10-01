using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillDto>> GetAllSkillsAsync();
        Task<SkillDto?> GetSkillByIdAsync(int id);
        Task AddSkillAsync(SkillDto skillDto);
        Task UpdateSkillAsync(SkillDto skillDto);
        Task DeleteSkillAsync(int id);
        Task AssignSkillToTraineeAsync(Guid traineeId, int skillId);
    }
}
