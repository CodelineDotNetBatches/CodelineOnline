using AuthenticationManagement.Models;

namespace AuthenticationManagement.Repositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        IQueryable<Role> Query();
    }
}
