using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IRoomRepository : IGenericRepo<Room>
    {
        IQueryable<Room> Query(); 

        Task<Room?> GetByNumberAsync(string roomNumber, bool includeBranch = false);
        Task<List<Room>> GetByBranchAsync(int branchId, bool includeBranch = false);
        Task<bool> ExistsAsync(string roomNumber);
    }
}
