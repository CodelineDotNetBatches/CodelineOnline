using AutoMapper;
using UserManagement.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using UserManagement.DTOs;

namespace UserManagement.Mapping
{
    // AutoMapper profile to configure mapping rules

    /// <summary>
    /// AutoMapper profile for Admin_Profile entity and DTO mapping.
    /// </summary>
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
