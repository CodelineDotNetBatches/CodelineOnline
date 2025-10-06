using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IInstructorSkillRepository
    {
        Task AddSkillAsync(InstructorSkill skill);
        Task AssignSkillToInstructorAsync(int instructorID, int skillId);
        Task<IEnumerable<InstructorSkill>> GetAllSkillsAsync();
        Task<InstructorSkill?> GetSkillByIdAsync(int Instructor_Id);
        Task<IEnumerable<InstructorSkill>> GetSkillsByInstructorAsync(int instructorID);
        Task RemoveSkillFromInstructorAsync(int InstructorID, int skillId);
        Task SaveAsync();
        void UpdateSkill(InstructorSkill skill);
    }
}