// Services/RoomService.cs
using UserManagement.DTOs;

namespace UserManagement.Services
{
    public interface IRoomService
    {
        Task<RoomDTO> CreateAsync(RoomDTO dto);
        Task<bool> DeleteAsync(string roomNumber);
        Task<List<RoomDTO>> GetAllAsync(bool includeBranch = false);
        Task<List<RoomDTO>> GetByBranchAsync(int branchId, bool includeBranch = false);
        Task<RoomDTO?> GetByNumberAsync(string roomNumber, bool includeBranch = false);
        Task<bool> UpdateAsync(string roomNumber, RoomDTO dto);
    }
}