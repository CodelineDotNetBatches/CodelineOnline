using AuthenticationManagement.DTOs;
using AuthenticationManagement.Models;
using AutoMapper;

namespace AuthenticationManagement.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleType, opt => opt.MapFrom(src => src.Role.RoleType));

            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}
