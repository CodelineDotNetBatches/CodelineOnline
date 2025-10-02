namespace CoursesManagement.Repos
{
    /// <summary>
    /// Generic repository interface that defines common CRUD operations 
    /// and persistence helpers for any entity type.
    /// </summary>
    public interface IGenericRepo<T> where T : class
    {
        /// <summary>
        /// Retrieves all entities as an IQueryable (supports deferred execution).
        /// </summary>
        IQueryable<T> GetAll();

        /// <summary>
        /// Finds an entity by its primary key asynchronously.
        /// Returns null if not found.
        /// </summary>
        Task<T?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity asynchronously.
        /// </summary>
        Task AddAsync(T entity);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        void Update(T entity);

        /// <summary>
        /// Deletes an existing entity.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        /// Provides direct access to IQueryable for advanced LINQ queries.
        /// </summary>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Saves all pending changes asynchronously to the database.
        /// </summary>
        Task SaveAsync();
    }
}
