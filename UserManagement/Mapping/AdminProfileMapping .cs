using UserManagement.DTOs;
using UserManagement.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;

namespace UserManagement.Mapping
{
    // AutoMapper profile to configure mapping rules
    public class AdminProfileMapping : Profile
    {
        public AdminProfileMapping()
        {
            // Map from Entity -> DTO
            CreateMap<Admin_Profile, AdminProfileDTO>();

            // Map from DTO -> Entity
            CreateMap<AdminProfileDTO, Admin_Profile>();
        }
    }
}
