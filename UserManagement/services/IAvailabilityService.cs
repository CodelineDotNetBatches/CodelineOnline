using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IAvailabilityService
    {
        Task<AvailabilityReadDto> AddAsync(AvailabilityCreateDto dto, CancellationToken ct = default);
        Task<IEnumerable<AvailabilityReadDto>> GenerateCalendarAsync(int instructorId, CancellationToken ct = default);
        Task<IEnumerable<AvailabilityReadDto>> GetByInstructorAsync(int instructorId, CancellationToken ct = default);
        Task RemoveAsync(int instructorId, int availabilityId, CancellationToken ct = default);
        Task<AvailabilityReadDto> UpdateAsync(int instructorId, int availabilityId, AvailabilityUpdateDto dto, CancellationToken ct = default);
    }
}