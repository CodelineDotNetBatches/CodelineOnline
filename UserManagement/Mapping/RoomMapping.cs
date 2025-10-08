using AutoMapper;
using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.mapping
{
    public class RoomMappingProfile : Profile
    {
        public RoomMappingProfile()
        {
            CreateMap<Room, RoomDTO>().ReverseMap();
        }
    }

}