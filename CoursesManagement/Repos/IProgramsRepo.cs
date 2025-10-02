using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface IProgramsRepo
    {
        Task<Programs?> GetProgramWithCoursesAsync(Guid programId);

        Task<bool> ExistsByNameAsync(string programName, Guid? excludeId = null);
        Task<IQueryable<Programs>> GetAllWithDetailsAsync();
        Task<IQueryable<Programs?>> GetByIdAsync(Guid id);
        Task<Programs?> GetByNameAsync(string programName);
    }
}