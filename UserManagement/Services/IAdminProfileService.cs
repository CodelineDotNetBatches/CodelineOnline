using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IAdminProfileService
    {
        // CREATE
        void AddAdminProfile(AdminProfileDTO adminDto);
        Task AddAdminAsync(AdminProfileDTO adminDto);

        // READ
        IEnumerable<AdminProfileDTO> GetAllAdmins();
        Task<IEnumerable<AdminProfileDTO>> GetAllAdminsAsync();
        AdminProfileDTO GetAdminById(int id);
        Task<AdminProfileDTO> GetAdminByIdAsync(int id);

        // UPDATE
        bool UpdateAdminProfile(AdminProfileDTO adminDto);
        Task<bool> UpdateAdminAsync(AdminProfileDTO adminDto);

        // DELETE
        bool DeleteAdminProfile(int id);
        Task<bool> DeleteAdminAsync(int id);

        // RESPONSIBILITIES
        void AddResponsibility(Responsibility responsibility);
        Task AddResponsibilityAsync(Responsibility responsibility);
        void UpdateResponsibility(Responsibility responsibility);
        Task UpdateResponsibilityAsync(Responsibility responsibility);
    }
}
