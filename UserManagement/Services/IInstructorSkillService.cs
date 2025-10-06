using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IInstructorSkillService
    {
        Task AddSkillAsync(InsSkillDto dto);
        Task DeleteSkillAsync(int id);
        Task<IEnumerable<InsSkillDto>> GetAllSkillsAsync();
        Task<InsSkillDto?> GetSkillByIdAsync(int id);
        Task UpdateSkillAsync(InsSkillDto dto);
    }
}