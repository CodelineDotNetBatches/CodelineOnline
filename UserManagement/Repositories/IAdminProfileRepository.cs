using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IAdminProfileRepository
    {
        void AddAdmin(Admin_Profile admin);
        Task AddAdminAsync(Admin_Profile admin);
        Admin_Profile GetAdminById(int id);
        Task<Admin_Profile> GetAdminByIdAsync(int id);
        IQueryable<Admin_Profile> GetAllAdmins();
        Task<IEnumerable<Admin_Profile>> GetAllAdminsAsync();
    }
}