using AutoMapper;
using UserManagement.Models;
using UserManagement.DTOs;

namespace UserManagement.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<TraineeSkill, TraineeSkillDto>().ReverseMap();
        }
    }
}
