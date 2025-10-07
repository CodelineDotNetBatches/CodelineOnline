using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using static ReportsManagements.DTOs.TrainerReportDtos;

namespace ReportsManagements.Mapping
{
    public class TrainerReportMapping : Profile
    {
        public TrainerReportMapping()
        {
            CreateMap<TrainerReportCreateDto, TrainerReport>();
            CreateMap<TrainerReport, TrainerReportResponseDto>();
            CreateMap<TrainerReportUpdateDto, TrainerReport>();
        }
    }
}
