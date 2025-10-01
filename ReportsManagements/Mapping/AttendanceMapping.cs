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
        }

    }
}

