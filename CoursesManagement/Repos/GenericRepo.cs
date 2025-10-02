using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// A generic repository that provides common CRUD operations 
    /// for any entity type using Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The entity type (must be a class).</typeparam>
    public class GenericRepo<T> where T : class
    {
        /// <summary>
        /// Database context for performing operations.
        /// </summary>
        protected readonly CoursesDbContext _context;

        /// <summary>
        /// Represents the set of entities in the context.
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes the repository with the specified database context.
        /// </summary>
        public GenericRepo(CoursesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Returns all entities as an IQueryable (supports deferred execution).
        /// </summary>
        public IQueryable<T> GetAll() => _dbSet;

        /// <summary>
        /// Finds an entity by its primary key asynchronously.
        /// Returns null if not found.
        /// </summary>
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Adds a new entity to the context asynchronously.
        /// </summary>
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        /// <summary>
        /// Marks an entity as modified for updating.
        /// </summary>
        public void Update(T entity) => _dbSet.Update(entity);

        /// <summary>
        /// Marks an entity for deletion from the context.
        /// </summary>
        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}
