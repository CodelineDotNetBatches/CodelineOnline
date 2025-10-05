using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Repositories
{
    public class GenericRepository<T> : IGenericRepo<T> where T : class
    {
        protected readonly UsersDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(UsersDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
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

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
