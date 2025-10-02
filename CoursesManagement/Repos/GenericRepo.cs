using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// A generic repository that provides common CRUD operations 
    /// for any entity type using Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The entity type (must be a class).</typeparam>
    public class GenericRepo<T> : IGenericRepo<T> where T : class
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
        /// <param name="context">The EF Core database context.</param>
        public GenericRepo(CoursesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> 
        /// as an <see cref="IQueryable{T}"/> for deferred execution and LINQ queries.
        /// </summary>
        /// <returns>IQueryable of all entities.</returns>
        public IQueryable<T> GetAll() => _dbSet;

        /// <summary>
        /// Finds an entity by its primary key asynchronously.
        /// Works for Guid, int, or any supported key type.
        /// </summary>
        /// <param name="id">The primary key value of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// Adds a new entity instance to the context asynchronously.
        /// </summary>
        /// <param name="entity">The entity instance to add.</param>
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        /// <summary>
        /// Marks an entity as modified in the context so that changes 
        /// are persisted on the next <see cref="SaveAsync"/>.
        /// </summary>
        /// <param name="entity">The entity instance to update.</param>
        public void Update(T entity) => _dbSet.Update(entity);

        /// <summary>
        /// Marks an entity for deletion from the context.
        /// It will be removed from the database on the next <see cref="SaveAsync"/>.
        /// </summary>
        /// <param name="entity">The entity instance to delete.</param>
        public void Delete(T entity) => _dbSet.Remove(entity);

        /// <summary>
        /// Provides direct access to <see cref="IQueryable{T}"/> 
        /// for advanced LINQ queries (including filters and joins).
        /// </summary>
        /// <returns>An IQueryable for custom queries.</returns>
        public IQueryable<T> GetQueryable() => _dbSet.AsQueryable();

        /// <summary>
        /// Saves all pending changes asynchronously to the database.
        /// </summary>
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
