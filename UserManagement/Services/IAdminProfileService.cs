using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IAdminProfileService
    {
        Task AddAdminAsync(AdminProfileDTO adminDto);
        void AddAdminProfile(AdminProfileDTO adminDto);
        void AddResponsibility(Responsibility responsibility);
        Task AddResponsibilityAsync(Responsibility responsibility);
        AdminProfileDTO GetAdminById(int id);
        Task<AdminProfileDTO> GetAdminByIdAsync(int id);
        IEnumerable<AdminProfileDTO> GetAllAdmins();
        Task<IEnumerable<AdminProfileDTO>> GetAllAdminsAsync();
        void UpdateResponsibility(Responsibility responsibility);
        Task UpdateResponsibilityAsync(Responsibility responsibility);
    }
}