using System.Linq;

namespace CoursesManagement.Repos
{
    /// <summary>
    /// Generic repository interface that defines common CRUD operations 
    /// and persistence helpers for any entity type.
    /// </summary>
    /// <typeparam name="T">The entity type (must be a class).</typeparam>
    public interface IGenericRepo<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities of type <typeparamref name="T"/> 
        /// as an <see cref="IQueryable{T}"/> for deferred execution and LINQ queries.
        /// </summary>
        /// <returns>IQueryable of all entities.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Finds an entity by its primary key asynchronously.
        /// Works for Guid, int, or any supported key type.
        /// </summary>
        /// <param name="id">The primary key value of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        Task<T?> GetByIdAsync(object id);

        /// <summary>
        /// Adds a new entity instance to the context asynchronously.
        /// </summary>
        /// <param name="entity">The entity instance to add.</param>
        Task AddAsync(T entity);

        /// <summary>
        /// Marks an entity as modified in the context so that changes 
        /// are persisted on the next <see cref="SaveAsync"/>.
        /// </summary>
        /// <param name="entity">The entity instance to update.</param>
        void Update(T entity);

        /// <summary>
        /// Marks an entity for deletion from the context.
        /// It will be removed from the database on the next <see cref="SaveAsync"/>.
        /// </summary>
        /// <param name="entity">The entity instance to delete.</param>
        void Delete(T entity);

        /// <summary>
        /// Provides direct access to <see cref="IQueryable{T}"/> 
        /// for advanced LINQ queries (including filters and joins).
        /// </summary>
        /// <returns>An IQueryable for custom queries.</returns>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Saves all pending changes asynchronously to the database.
        /// </summary>
        Task SaveAsync();
    }
}
