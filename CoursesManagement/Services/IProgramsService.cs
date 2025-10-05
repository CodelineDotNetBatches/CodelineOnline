using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    public interface IProgramsService
    {
        Task<ProgramDetailsDto> CreateProgramAsync(ProgramCreateDto dto);
        Task DeleteProgramAsync(Guid id);
        Task<IEnumerable<ProgramDetailsDto>> GetAllProgramsAsync();
        Task<ProgramDetailsDto?> GetProgramByIdAsync(Guid id);
        Task UpdateProgramAsync(Guid id, ProgramUpdateDto dto);
    }
}