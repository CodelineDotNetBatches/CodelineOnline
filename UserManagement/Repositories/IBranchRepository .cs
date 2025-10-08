using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IBranchRepository : IGenericRepo<Branch>
    {
        IQueryable<Branch> Query(bool includeNavs = false);

        Task<Branch?> GetByIdWithNavsAsync(int id);

        Task<bool> BranchNameExistsAsync(string branchName, int? excludeId = null);

        Task AddPhoneAsync(int branchId, int phoneNumber= default);

        Task RemovePhoneAsync(int branchId, int phoneNumbert = default);
    }
}
