using AutoMapper;
using ReportsManagements.DTOs;
using ReportsManagements.Models;

namespace ReportsManagements.Mapping
{
    public class AttendanceMapping : Profile
    {

        public AttendanceMapping()
        {
            CreateMap<AttendanceRecord, AttendanceRecordDto>();
            CreateMap<AttendanceRecordCreateDto, AttendanceRecord>();
            CreateMap<AttendanceRecordUpdateDto, AttendanceRecord>();
            CreateMap<AttendanceRecordCreateDto, AttendanceRecord>()
                .ForMember(d => d.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<AttendanceRecordUpdateDto, AttendanceRecord>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<AttendanceRecord, AttendanceRecordDto>();
        }

    }
}

