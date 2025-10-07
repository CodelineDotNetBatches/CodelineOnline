using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;

namespace ReportsManagements.Mapping
{
    public class BranchReportMapping : Profile
    {
        public BranchReportMapping()
        {
            CreateMap<BranchReportDtos.BranchReportCreateDto, BranchReport>();
            CreateMap<BranchReport, BranchReportDtos.BranchReportResponseDto>();

        }
    }
}
