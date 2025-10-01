using AuthenticationManagement.DTOs;
using AuthenticationManagement.Models;
using AuthenticationManagement.Repositories;
using AutoMapper;

namespace AuthenticationManagement.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            await _repository.AddAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto dto)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return null;

            _mapper.Map(dto, role);
            await _repository.UpdateAsync(role);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);
            if (role == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
