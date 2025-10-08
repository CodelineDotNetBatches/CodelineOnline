using AutoMapper;
using UserManagement.DTOs;
using UserManagement.Models;

namespace UserManagement.mapping
{
    public class InstructorMappingProfile : Profile
    {
        public InstructorMappingProfile()
        {
            CreateMap<Instructor, InstructorReadDto>().ReverseMap();
            CreateMap<InstructorCreateDto, Instructor>();
        }
    }

}
