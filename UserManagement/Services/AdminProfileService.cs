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




    }
}
