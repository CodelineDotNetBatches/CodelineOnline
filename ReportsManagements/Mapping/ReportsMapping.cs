using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;


namespace ReportsManagements.Mapping
{
    public class ReportsMapping : Profile
    {
        public ReportsMapping()
        {
            CreateMap<TrainerReportUpsertDto, TrainerReport>()
                .ForMember(d => d.TrainerReportId, o => o.Ignore());

            CreateMap<CourseReportUpsertDto, CourseReport>()
                .ForMember(d => d.CourseReportId, o => o.Ignore());
        }
    }
}
