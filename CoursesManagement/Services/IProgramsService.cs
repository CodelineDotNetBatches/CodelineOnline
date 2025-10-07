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

        Task<ProgramDetailsDto?> GetProgramByNameAsync(string programName);

        Task<ProgramDetailsDto?> GetProgramWithCoursesAsync(Guid programId);

        Task<ProgramDetailsDto?> GetProgramWithCategoriesAsync(Guid programId);

    }
}