using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Repositories
{
    /// <summary>
    /// Generic repository interface defining common CRUD operations.
    /// </summary>
    public interface IGenericRepo<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
