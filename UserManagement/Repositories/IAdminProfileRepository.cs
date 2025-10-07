using UserManagement.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserManagement.Repositories
{
    public interface IAdminProfileRepository
    {
        // CREATE
        void AddAdminProfile(Admin_Profile admin);
        Task AddAdminProfileAsync(Admin_Profile admin);

        void AddResponsibility(Responsibility responsibility);
        Task AddResponsibilityAsync(Responsibility responsibility);

        // READ
        IQueryable<Admin_Profile> GetAllAdmins();
        Task<IQueryable<Admin_Profile>> GetAllAdminsAsync();

        Admin_Profile GetAdminById(int id);
        Task<Admin_Profile> GetAdminByIdAsync(int id);

        // UPDATE
        void UpdateAdminProfile(Admin_Profile admin);
        Task UpdateAdminProfileAsync(Admin_Profile admin);

        void UpdateResponsibility(Responsibility responsibility);
        Task UpdateResponsibilityAsync(Responsibility responsibility);

        // DELETE
        void DeleteAdminProfile(Admin_Profile admin);
        Task DeleteAdminProfileAsync(Admin_Profile admin);
    }
}
