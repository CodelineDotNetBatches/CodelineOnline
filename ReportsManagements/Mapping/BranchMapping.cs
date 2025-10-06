using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;

namespace ReportsManagements.Mapping
{
    public class Branch_mapping: Profile
    {
        public Branch_mapping()
        {
            CreateMap<BranchDtos.BranchCreateDto, Branch>();
            CreateMap<Branch, BranchDtos.BranchResponseDto>();
        }
    }
}
