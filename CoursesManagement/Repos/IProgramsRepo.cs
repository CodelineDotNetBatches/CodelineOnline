using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface IProgramsRepo : IGenericRepo<Program>
    {
        Task<IEnumerable<Program>> GetProgramsByCategoryAsync(int categoryId);
        Task<IEnumerable<Program>> GetProgramsByLevelAsync(LevelType level);
        Task<Program?> GetProgramWithCategoryAsync(Guid programId);
    }
}