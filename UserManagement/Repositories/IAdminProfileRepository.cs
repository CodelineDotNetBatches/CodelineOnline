using UserManagement.Models;

namespace UserManagement.Repositories
{
    public interface IAdminProfileRepository
    {
        void AddAdminProfile(Admin_Profile admin);
        Task AddAdminProfileAsync(Admin_Profile admin);
        void AddResponsibility(Responsibility responsibility);
        Task AddResponsibilityAsync(Responsibility responsibility);
        Admin_Profile GetAdminById(int id);
        Task<Admin_Profile> GetAdminByIdAsync(int id);
        IQueryable<Admin_Profile> GetAllAdmins();
        Task<IQueryable<Admin_Profile>> GetAllAdminsAsync();
        void UpdateResponsibility(Responsibility responsibility);
        Task UpdateResponsibilityAsync(Responsibility responsibility);
    }
}