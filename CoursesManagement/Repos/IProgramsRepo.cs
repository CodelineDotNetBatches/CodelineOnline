using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface IProgramsRepo : IGenericRepo<Program>
    {
        Task<Programs?> GetProgramWithCoursesAsync(Guid programId);
    }
}