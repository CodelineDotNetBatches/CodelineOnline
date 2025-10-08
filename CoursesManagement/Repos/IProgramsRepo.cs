using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface IProgramsRepo : IGenericRepo<Programs>
    {
        Task<Programs?> GetProgramWithCoursesAsync(Guid programId);

        Task<bool> ExistsByNameAsync(string programName, Guid? excludeId = null);
        Task<List<Programs>> GetAllWithDetailsAsync();
        Task<Programs?> GetByIdAsync(Guid id);
        Task<Programs?> GetByNameAsync(string programName);

        Task<Programs?> GetProgramWithCategoriesAsync(Guid programId);
        Task<Programs?> GetProgramWithEnrollmentsAsync(Guid programId);
    }
}