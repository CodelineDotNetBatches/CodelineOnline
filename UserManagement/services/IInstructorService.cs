using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IInstructorService
    {
        Task<InstructorReadDto> CreateFromUserAsync(InstructorCreateDto dto);
        Task DeleteAsync(int id);
        IEnumerable<InstructorReadDto> GetAll(PagingFilter filter);
        Task<IEnumerable<InstructorReadDto>> GetAllAsync(PagingFilter filter);
        Task<InstructorReadDto?> GetAsync(int id);
        Task<InstructorReadDto> UpdateAsync(int id, InstructorUpdateDto dto);
    }
}