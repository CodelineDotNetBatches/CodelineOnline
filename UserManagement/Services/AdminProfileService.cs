using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Repositories;

namespace UserManagement.Services
{
    public class AdminProfileService : IAdminProfileService
    {
        private readonly AdminProfileRepository _repository; // Reference to repository
        private readonly IMapper _mapper;                    // Reference to AutoMapper

        // Constructor injection: Repository + IMapper are injected by Dependency Injection
        public AdminProfileService(AdminProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // -------------------
        // SYNC METHODS
        // -------------------

        //Get all AdminProfile (sync) and return DTOs
        /// Uses IEnumerable since the data is already materialized

        public IEnumerable<AdminProfileDTO> GetAllAdmins()
        {
            var admins = _repository.GetAllAdmins().ToList();       // Get entities
            return _mapper.Map<IEnumerable<AdminProfileDTO>>(admins); // Map to DTOs
        }

        // Get AdminProfile by Id (sync)

        public AdminProfileDTO GetAdminById(int id)
        {
            var admin = _repository.GetAdminById(id);              // Get entity
            return _mapper.Map<AdminProfileDTO>(admin);            // Map to DTO
        }

        // Add new AdminProfile (sync)

        public void AddAdminProfile(AdminProfileDTO adminDto)
        {
            var adminEntity = _mapper.Map<Admin_Profile>(adminDto); // Map DTO -> Entity
            _repository.AddAdminProfile(adminEntity);               // Save to DB
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

        // Get AdminProfile by Id (async) and return DTOs
        // Returns IEnumerable since data is materialized
        public async Task<IEnumerable<AdminProfileDTO>> GetAllAdminsAsync()
        {
            var admins = await _repository.GetAllAdminsAsync();         // Get entities
            return _mapper.Map<IEnumerable<AdminProfileDTO>>(admins);   // Map to DTOs
        }


        // Get AdminProfile by Id (async) 
        public async Task<AdminProfileDTO> GetAdminByIdAsync(int id)
        {
            var admin = await _repository.GetAdminByIdAsync(id);        // Get entity
            return _mapper.Map<AdminProfileDTO>(admin);                 // Map to DTO
        }



        //Add new AdminProfile (async)
        public async Task AddAdminAsync(AdminProfileDTO adminDto)
        {
            var adminEntity = _mapper.Map<Admin_Profile>(adminDto);     // Map DTO -> Entity
            await _repository.AddAdminProfileAsync(adminEntity);        // Save to DB
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
