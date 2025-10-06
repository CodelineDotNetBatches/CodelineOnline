using AutoMapper;
using CoursesManagement.Models;
using CoursesManagement.DTOs;

namespace CoursesManagement.Mapping
{
    /// <summary>
    /// Central AutoMapper profile for the Courses Management System.
    /// Defines mappings between all domain entities and their DTOs.
    /// Comments from DTOs are reused where applicable for clarity.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // =========================================================
            //  PROGRAM MAPPINGS
            // =========================================================
            CreateMap<Programs, ProgramDetailsDto>()
                .ForMember(dest => dest.CategoryNames,
                           opt => opt.MapFrom(src => src.Categories.Select(c => c.CategoryName)))
                .ForMember(dest => dest.CourseNames,
                           opt => opt.MapFrom(src => src.Courses.Select(c => c.CourseName)))
                .ForMember(dest => dest.CreatedAtUtc,
                           opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.EnrollmentsCount,
                           opt => opt.MapFrom(src => src.Enrollments.Count));

            CreateMap<ProgramCreateDto, Programs>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Courses, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProgramUpdateDto, Programs>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.Courses, opt => opt.Ignore())
                .ReverseMap();

            // =========================================================
            //  CATEGORY MAPPINGS
            // =========================================================
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.Programs, opt => opt.MapFrom(src => src.Programs))
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
                .ReverseMap();

            CreateMap<Category, CategoryDetailDto>()
                .ForMember(dest => dest.Courses, opt => opt.MapFrom(src => src.Courses))
                .ReverseMap();

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(dest => dest.Programs, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateCategoryDto, Category>()
                .ForMember(dest => dest.Programs, opt => opt.Ignore())
                .ReverseMap();

            // =========================================================
            //  COURSE MAPPINGS
            // =========================================================
            CreateMap<Course, CourseListDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Course, CourseDetailsDto>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<CourseCreateDto, Course>()
                .ForMember(dest => dest.CategoryId,
                           opt => opt.MapFrom(src => src.CategoryId))
                .ReverseMap();

            CreateMap<CourseUpdateDto, Course>()
                .ForMember(dest => dest.CategoryId,
                           opt => opt.MapFrom(src => src.CategoryId))
                .ReverseMap();

            // =========================================================
            //  ENROLLMENT MAPPINGS
            // =========================================================
            CreateMap<Enrollment, EnrollmentListDto>()
                //.ForMember(dest => dest.UserName,
                //           opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CourseTitle,
                           opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.StatusChangeReason,
                           opt => opt.MapFrom(src => src.StatusChangeReason));

            CreateMap<Enrollment, EnrollmentDetailDto>()
                //.ForMember(dest => dest.UserName,
                //           opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.CourseTitle,
                           opt => opt.MapFrom(src => src.Course.CourseName))
                .ForMember(dest => dest.ProgramName,
                           opt => opt.MapFrom(src => src.Program != null ? src.Program.ProgramName : null))
                .ForMember(dest => dest.EnrolledAt,
                           opt => opt.MapFrom(src => src.EnrolledAt))
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Grade,
                           opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.StatusChangeReason,
                           opt => opt.MapFrom(src => src.StatusChangeReason));

            CreateMap<CreateEnrollmentDto, Enrollment>().ReverseMap();
            CreateMap<UpdateEnrollmentDto, Enrollment>().ReverseMap();

            // =========================================================
            //  CERTIFICATE MAPPINGS
            // =========================================================
            //CreateMap<Certificate, CertificateListItemDto>().ReverseMap();
            //CreateMap<Certificate, CertificateDetailsDto>()
            //    .ForMember(dest => dest.CourseName,
            //               opt => opt.MapFrom(src => src.Course.CourseName))
            //    //.ForMember(dest => dest.UserName,
            //    //           opt => opt.MapFrom(src => src.User.FullName))
            //    .ForMember(dest => dest.IssuedDate,
            //               opt => opt.MapFrom(src => src.IssuedAt))
            //    .ReverseMdap();

            //CreateMap<CertificateIssueDto, Certificate>()
            //    .ForMember(dest => dest.IssuedAt,
            //               opt => opt.MapFrom(_ => DateTime.UtcNow))
            //    .ReverseMap();

            //CreateMap<CertificateUpdateUrlDto, Certificate>()
            //    .ForMember(dest => dest.CertificateUrl,
            //               opt => opt.MapFrom(src => src.CertificateUrl))
            //    .ReverseMap();

            //CreateMap<CertificateQueryDto, Certificate>().ReverseMap();
            //CreateMap<CertificateVerifyResultDto, Certificate>().ReverseMap();

            //// =========================================================
            ////  USER MAPPINGS
            //// =========================================================
            //CreateMap<User, UserDto>().ReverseMap();
            //CreateMap<CreateUserDto, User>().ReverseMap();
            //CreateMap<UpdateUserDto, User>().ReverseMap();
        }
    }
}
