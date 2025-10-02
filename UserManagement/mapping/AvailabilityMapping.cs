using AutoMapper;
using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.mapping
{


    public class AvailabilityMappingProfile : Profile
    {
        public AvailabilityMappingProfile()
        {
            CreateMap<Availability, AvailabilityReadDto>().ReverseMap();
            CreateMap<AvailabilityCreateDto, Availability>();
            CreateMap<AvailabilityUpdateDto, Availability>();
        }
    }
}