// Services/IRoomService.cs
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IRoomService
    {
        Task<List<Room>> GetAllAsync(bool includeBranch = false);
        Task<Room?> GetByNumberAsync(string roomNumber, bool includeBranch = false);
        Task<List<Room>> GetByBranchAsync(int branchId, bool includeBranch = false);

        Task<Room> CreateAsync(Room room);
        Task<bool> UpdateAsync(string roomNumber, Room updated);
        Task<bool> DeleteAsync(string roomNumber);
    }
}
