using AutoMapper;
using ReportsManagements.Models;
using ReportsManagements.DTOs;

namespace ReportsManagements.Mapping
{
    public class CourseReportMapping: Profile
    {
        public CourseReportMapping()
        {
            CreateMap<CourseReportCreateDto, CourseReport>();
            CreateMap<CourseReport, CourseReportResponseDto>();
            CreateMap<CourseReportUpdateDto, CourseReport>();
        }
    }
}
