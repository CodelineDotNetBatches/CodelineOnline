using AuthenticationManagement.Models;
using System;

namespace AuthenticationManagement.Repositories
{
    public class RoleRepository : GenericRepository<Role> , IRoleRepository
    {
        public RoleRepository(AuthenticationDbContext context) : base(context) { }
    }
}
