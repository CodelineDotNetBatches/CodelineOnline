using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class AdminProfileService
    {
        private readonly AdminProfileRepository _repository; // Reference to repository

        // Constructor injection: Repository is passed in from Dependency Injection (DI)
        public AdminProfileService(AdminProfileRepository repository)
        {
            _repository = repository; // Store reference for use in methods
        }

        // -------------------
        // SYNC METHODS
        // -------------------

        //Get all AdminProfile (sync)
        /// Uses IQueryable for flexibility in filtering

        public IQueryable<Admin_Profile> GetAllAdmins()
        {
            // Calls repository sync method 
            return _repository.GetAllAdmins();
        }

        // Get AdminProfile by Id (sync)

        public Admin_Profile GetAdminById(int id)
        {
            return _repository.GetAdminById(id); // Calls repository sync method 
        }

        // Add new AdminProfile (sync)

        public void AddAdmin(Admin_Profile admin)
        {
            _repository.AddAdmin(admin); // Calls repository sync method
        }
}
