using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IInstructorService
    {
        Task<InstructorReadDto> CreateFromUserAsync(InstructorCreateDto dto, CancellationToken ct = default);
        Task DeleteAsync(int id, CancellationToken ct = default);
        IEnumerable<InstructorReadDto> GetAll(PagingFilter filter);
        Task<IEnumerable<InstructorReadDto>> GetAllAsync(PagingFilter filter, CancellationToken ct = default);
        Task<InstructorReadDto?> GetAsync(int id, CancellationToken ct = default);
        Task<InstructorReadDto> UpdateAsync(int id, InstructorUpdateDto dto, CancellationToken ct = default);
    }
}