using AutoMapper;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Service implementation for managing enrollments.
    /// Handles mapping, validation, and repository operations.
    /// </summary>
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Initializes a new instance of EnrollmentService.
        /// </summary>
        /// <param name="repo">The enrollment repository (data access layer).</param>
        /// <param name="mapper">The AutoMapper instance for DTO mapping.</param>
        public EnrollmentService(IEnrollmentRepo repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EnrollmentListDto>> GetAllAsync()
        {
            var enrollments = await _repo.GetAll()
                .Include(e => e.User)
                .Include(e => e.Course)
                .Include(e => e.Program)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
        }

        /// <inheritdoc />
        public async Task<EnrollmentDetailDto?> GetByIdAsync(Guid enrollmentId)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return null;

            // Ensure navigation properties are loaded
            await _repo._context.Entry(enrollment).Reference(e => e.User).LoadAsync();
            await _repo._context.Entry(enrollment).Reference(e => e.Course).LoadAsync();
            await _repo._context.Entry(enrollment).Reference(e => e.Program).LoadAsync();

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        /// <inheritdoc />
        public async Task<EnrollmentDetailDto?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            var enrollment = await _repo.GetByUserAndCourseAsync(userId, courseId);
            return enrollment == null ? null : _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EnrollmentListDto>> GetByUserIdAsync(Guid userId)
        {
            var enrollments = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<EnrollmentListDto>> GetByCourseIdAsync(Guid courseId)
        {
            var enrollments = await _repo.GetByCourseIdAsync(courseId);
            return _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
        }

        /// <inheritdoc />
        public async Task<EnrollmentDetailDto> CreateAsync(CreateEnrollmentDto dto)
        {
            var existing = await _repo.GetByUserAndCourseAsync(dto.UserId, dto.CourseId);
            if (existing != null)
                throw new InvalidOperationException("User is already enrolled in this course.");

            var enrollment = _mapper.Map<Enrollment>(dto);

            await _repo.AddAsync(enrollment);
            await _repo.SaveAsync();

            // Load related entities for mapping
            await _repo._context.Entry(enrollment).Reference(e => e.Course).LoadAsync();
            await _repo._context.Entry(enrollment).Reference(e => e.Program).LoadAsync();
            await _repo._context.Entry(enrollment).Reference(e => e.User).LoadAsync();

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        /// <inheritdoc />
        public async Task<EnrollmentDetailDto?> UpdateAsync(Guid enrollmentId, UpdateEnrollmentDto dto)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return null;

            _mapper.Map(dto, enrollment);

            _repo.Update(enrollment);
            await _repo.SaveAsync();

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(Guid enrollmentId)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return false;

            _repo.Delete(enrollment);
            await _repo.SaveAsync();

            return true;
        }
    }
}
