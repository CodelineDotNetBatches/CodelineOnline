using AutoMapper;
using UserManagement.Models;
using UserManagement.DTOs;

namespace UserManagement.Mapping
{


    /// <summary>
    /// AutoMapper profile for InstructorSkill ↔ SkillDto mapping.
    /// </summary>
    public class InsSkillsMapping : Profile
    {
        public InsSkillsMapping()
        {
            //  Automatically handles both directions
            CreateMap<InstructorSkill, InsSkillDto>().ReverseMap();
        }
    }
}