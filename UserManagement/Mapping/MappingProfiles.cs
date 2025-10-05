using AutoMapper;
using UserManagement.Models;
using UserManagement.DTOs;

namespace UserManagement.Mapping
{
    public class SkillTraineeMappingProfiles : Profile
    {
        public SkillTraineeMappingProfiles()
        {
            CreateMap<Skill, SkillDto>().ReverseMap();
        }
    }
}
