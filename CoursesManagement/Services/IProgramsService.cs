using CoursesManagement.DTOs;
using CoursesManagement.Models;

namespace CoursesManagement.Services
{
    public interface IProgramsService
    {
        Task<ProgramDetailsDto> CreateProgramAsync(ProgramCreateDto dto);
        Task DeleteProgramAsync(Guid id);
        Task<IEnumerable<ProgramDetailsDto>> GetAllProgramsAsync();
        Task<ProgramDetailsDto?> GetProgramByIdAsync(Guid id);
        Task UpdateProgramAsync(Guid id, ProgramUpdateDto dto);

        Task<Programs?> GetProgramByNameAsync(string programName);

        Task<Programs?> GetProgramWithCoursesAsync(Guid programId);

        Task<Programs?> GetProgramWithCategoriesAsync(Guid programId);

    }
}