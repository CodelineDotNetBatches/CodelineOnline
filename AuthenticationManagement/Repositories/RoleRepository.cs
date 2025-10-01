using System;
using System.Collections.Generic;
using AuthenticationManagement.Models;
namespace AuthenticationManagement.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AuthenticationDbContext context) : base(context) { }
    }

    
}
