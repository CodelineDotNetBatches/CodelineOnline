using UserManagement.Models;

namespace UserManagement.Repositories
{
    /// <summary>
    /// Repository interface for managing Skill operations
    /// across both Trainees and Instructors.
    /// </summary>
    public interface ISkillRepository : IGenericRepo<Skill>
    {
        // ============================================================
        // 🔹 Common Skill Operations
        // ============================================================

        /// <summary>
        /// Retrieves all skills asynchronously.
        /// </summary>
        Task<IEnumerable<Skill>> GetAllSkillsAsync();

        /// <summary>
        /// Retrieves a skill by its ID asynchronously.
        /// </summary>
        Task<Skill?> GetSkillByIdAsync(int id);

        // ============================================================
        // 🔹 Trainee Skill Operations
        // ============================================================

        /// <summary>
        /// Gets all skills assigned to a specific trainee.
        /// </summary>
        Task<IEnumerable<Skill>> GetSkillsByTraineeAsync(Guid traineeId);

        /// <summary>
        /// Assigns a skill to a trainee.
        /// </summary>
        Task AssignSkillToTraineeAsync(Guid traineeId, int skillId);

        // ============================================================
        // 🔹 Instructor Skill Operations
        // ============================================================

        /// <summary>
        /// Gets all skills assigned to a specific instructor.
        /// </summary>
        Task<IEnumerable<Skill>> GetSkillsByInstructorAsync(Guid userId);

        /// <summary>
        /// Assigns a skill to an instructor.
        /// </summary>
        Task AssignSkillToInstructorAsync(int userId, int skillId);

        /// <summary>
        /// Removes a skill from an instructor.
        /// </summary>
        Task RemoveSkillFromInstructorAsync(int userId, int skillId);
    }
}
