using AuthenticationManagement.DTOs;
using AuthenticationManagement.Models;
using AutoMapper;

namespace AuthenticationManagement.Mapping
{
    public class RoleProfile: Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();
        }
    }
}
