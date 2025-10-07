using CoursesManagement.DTOs;
using CoursesManagement.Models;

namespace CoursesManagement.Services
{
    public interface IProgramsService
    {
        Task<ProgramDetailsDto> CreateProgramAsync(ProgramCreateDto dto);
        Task DeleteProgramAsync(Guid id);
        Task<IEnumerable<ProgramDetailsDto>> GetAllProgramsAsync();
        Task<ProgramsDto?> GetProgramByIdAsync(Guid id);
        Task UpdateProgramAsync(Guid id, ProgramUpdateDto dto);

        Task<ProgramsDto?> GetProgramByNameAsync(string programName);

        Task<ProgramsWithCoursesDto?> GetProgramWithCoursesAsync(Guid programId);

        Task<ProgramsWithCategoryDto?> GetProgramWithCategoriesAsync(Guid programId);

        Task<ProgramsWithEnrollmentDto?> GetProgramWithEnrollmentsAsync(Guid programId);
    }
}