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
        private readonly IAdminProfileRepository _repository;
        private readonly IMapper _mapper;

        public AdminProfileService(IAdminProfileRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // ======================================================
        // SYNC METHODS
        // ======================================================

        // GET ALL ADMINS
        public IEnumerable<AdminProfileDTO> GetAllAdmins()
        {
            var admins = _repository.GetAllAdmins().ToList();
            return _mapper.Map<IEnumerable<AdminProfileDTO>>(admins);
        }

        // GET ADMIN BY ID
        public AdminProfileDTO GetAdminById(int id)
        {
            var admin = _repository.GetAdminById(id);
            return _mapper.Map<AdminProfileDTO>(admin);
        }

        // ADD ADMIN
        public void AddAdminProfile(AdminProfileDTO adminDto)
        {
            var adminEntity = _mapper.Map<Admin_Profile>(adminDto);
            _repository.AddAdminProfile(adminEntity);
        }

        // UPDATE ADMIN
        public bool UpdateAdminProfile(AdminProfileDTO dto)
        {
            var existingAdmin = _repository.GetAdminById(dto.AdminId);
            if (existingAdmin == null)
                return false;

            _mapper.Map(dto, existingAdmin); // maps changes from DTO → entity
            _repository.UpdateAdminProfile(existingAdmin);
            return true;
        }

        // DELETE ADMIN
        public bool DeleteAdminProfile(int id)
        {
            var admin = _repository.GetAdminById(id);
            if (admin == null)
                return false;

            _repository.DeleteAdminProfile(admin);
            return true;
        }

        // ADD RESPONSIBILITY
        public void AddResponsibility(Responsibility responsibility)
        {
            _repository.AddResponsibility(responsibility);
        }

        // UPDATE RESPONSIBILITY
        public void UpdateResponsibility(Responsibility responsibility)
        {
            _repository.UpdateResponsibility(responsibility);
        }

        // ======================================================
        // ASYNC METHODS
        // ======================================================

        // GET ALL ADMINS (ASYNC)
        public async Task<IEnumerable<AdminProfileDTO>> GetAllAdminsAsync()
        {
            var admins = await _repository.GetAllAdminsAsync();
            return _mapper.Map<IEnumerable<AdminProfileDTO>>(admins);
        }

        // GET ADMIN BY ID (ASYNC)
        public async Task<AdminProfileDTO> GetAdminByIdAsync(int id)
        {
            var admin = await _repository.GetAdminByIdAsync(id);
            return _mapper.Map<AdminProfileDTO>(admin);
        }

        // ADD ADMIN (ASYNC)
        public async Task AddAdminAsync(AdminProfileDTO adminDto)
        {
            var adminEntity = _mapper.Map<Admin_Profile>(adminDto);
            await _repository.AddAdminProfileAsync(adminEntity);
        }

        // UPDATE ADMIN (ASYNC)
        public async Task<bool> UpdateAdminAsync(AdminProfileDTO dto)
        {
            var existingAdmin = await _repository.GetAdminByIdAsync(dto.AdminId);
            if (existingAdmin == null)
                return false;

            _mapper.Map(dto, existingAdmin);
            await _repository.UpdateAdminProfileAsync(existingAdmin);
            return true;
        }

        // DELETE ADMIN (ASYNC)
        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await _repository.GetAdminByIdAsync(id);
            if (admin == null)
                return false;

            await _repository.DeleteAdminProfileAsync(admin);
            return true;
        }

        // ADD RESPONSIBILITY (ASYNC)
        public async Task AddResponsibilityAsync(Responsibility responsibility)
        {
            await _repository.AddResponsibilityAsync(responsibility);
        }

        // UPDATE RESPONSIBILITY (ASYNC)
        public async Task UpdateResponsibilityAsync(Responsibility responsibility)
        {
            await _repository.UpdateResponsibilityAsync(responsibility);
        }
    }
}
