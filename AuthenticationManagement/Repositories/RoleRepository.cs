using AuthenticationManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthenticationManagement.Repositories
{
    public class RoleRepository : GenericRepository<Role> , IRoleRepository
    {
        private readonly AuthenticationDbContext _context;
        public RoleRepository(AuthenticationDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Role> Query() => _context.Roles.AsQueryable();
    }
}
