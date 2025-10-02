using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class AdminProfileService : IAdminProfileService
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
        /// Uses IEnumerable since the data is already materialized

        public IEnumerable<Admin_Profile> GetAllAdmins()
        {
            // Calls repository sync method and materializes data immediately
            return _repository.GetAllAdmins().ToList();
        }

        // Get AdminProfile by Id (sync)

        public Admin_Profile GetAdminById(int id)
        {
            return _repository.GetAdminById(id); // Calls repository sync method 
        }

        // Add new AdminProfile (sync)

        public void AddAdminProfile(Admin_Profile admin)
        {
            _repository.AddAdminProfile(admin); // Calls repository sync method
        }

        // Add a new Responsibility (sync)
        public void AddResponsibility(Responsibility responsibility)
        {
            // Calls repository sync method
            _repository.AddResponsibility(responsibility);
        }

        // Update an existing Responsibility (sync).

        public void UpdateResponsibility(Responsibility responsibility)
        {
            // Calls repository sync method
            _repository.UpdateResponsibility(responsibility);
        }


        // -------------------
        // ASYNC METHODS
        // -------------------

        // Get AdminProfile by Id (async)
        // Returns IEnumerable since data is materialized
        public async Task<IEnumerable<Admin_Profile>> GetAllAdminsAsync()
        {
            return await _repository.GetAllAdminsAsync(); // Calls repository async method
        }


        // Get AdminProfile by Id (async) 
        public async Task<Admin_Profile> GetAdminByIdAsync(int id)
        {
            // Calls repository async method
            return await _repository.GetAdminByIdAsync(id);
        }


        //Add new AdminProfile (async)
        public async Task AddAdminAsync(Admin_Profile admin)
        {
            // Calls repository async method
            await _repository.AddAdminProfileAsync(admin);
        }

        // Add a new Responsibility (async).

        public async Task AddResponsibilityAsync(Responsibility responsibility)
        {
            // Calls repository async method
            await _repository.AddResponsibilityAsync(responsibility);
        }

        // Update an existing Responsibility (async)
        public async Task UpdateResponsibilityAsync(Responsibility responsibility)
        {
            // Calls repository async method
            await _repository.UpdateResponsibilityAsync(responsibility);
        }
    }
}
