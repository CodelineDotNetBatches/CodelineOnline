using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IAdminProfileService
    {
        Task AddAdminAsync(Admin_Profile admin);
        void AddAdminProfile(Admin_Profile admin);
        void AddResponsibility(Responsibility responsibility);
        Task AddResponsibilityAsync(Responsibility responsibility);
        Admin_Profile GetAdminById(int id);
        Task<Admin_Profile> GetAdminByIdAsync(int id);
        IEnumerable<Admin_Profile> GetAllAdmins();
        Task<IEnumerable<Admin_Profile>> GetAllAdminsAsync();
        void UpdateResponsibility(Responsibility responsibility);
        Task UpdateResponsibilityAsync(Responsibility responsibility);
    }
}