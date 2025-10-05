using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Repos; // Adjust if your interface is under a different folder/namespace
using UserManagement.Models; // Optional if entities are here

namespace UserManagement.Repositories
{
    /// <summary>
    /// Generic repository implementation for common CRUD operations.
    /// </summary>
    /// <typeparam name="T">The entity type (e.g., User, Role, Skill).</typeparam>
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly UsersDbContext _context; // fixed from CoursesDbContext
        protected readonly DbSet<T> _dbSet;

        public GenericRepo(UsersDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
