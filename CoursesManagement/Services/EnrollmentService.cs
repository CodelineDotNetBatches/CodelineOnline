using AutoMapper;
using CoursesManagement.Caching;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Provides business logic and caching for managing enrollments.
    /// Includes methods for retrieving, creating, updating, and deleting enrollments.
    /// </summary>
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepo _repo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public EnrollmentService(IEnrollmentRepo repo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _mapper = mapper;
            _cache = cache;
        }

        // ======================================================
        // 🔹 GET ALL (with caching)
        // ======================================================
        /// <summary>
        /// Retrieves all enrollments, including related courses and programs.
        /// Results are cached for faster subsequent access.
        /// </summary>
        public async Task<IEnumerable<EnrollmentListDto>> GetAllAsync()
        {
            if (!_cache.TryGetValue(CacheKeys.AllEnrollments, out IEnumerable<EnrollmentListDto>? cachedEnrollments))
            {
                var enrollments = await _repo.GetAll()
                    .Include(e => e.Course)
                    .Include(e => e.Program)
                    //.Include(e => e.User) // Uncomment when User module is merged
                    .ToListAsync();

                cachedEnrollments = _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
                _cache.Set(CacheKeys.AllEnrollments, cachedEnrollments, TimeSpan.FromMinutes(10));
            }

            return cachedEnrollments!;
        }

        // ======================================================
        // 🔹 GET BY ID (with relations and caching)
        // ======================================================
        /// <summary>
        /// Retrieves a single enrollment by its unique ID.
        /// Includes related entities such as Course and Program.
        /// Cached for 10 minutes.
        /// </summary>
        public async Task<EnrollmentDetailDto?> GetByIdAsync(Guid enrollmentId)
        {
            var cacheKey = CacheKeys.EnrollmentsByCourse(enrollmentId);
            if (!_cache.TryGetValue(cacheKey, out EnrollmentDetailDto? cached))
            {
                var enrollment = await _repo.GetByIdWithRelationsAsync(enrollmentId);
                if (enrollment == null) return null;

                cached = _mapper.Map<EnrollmentDetailDto>(enrollment);
                _cache.Set(cacheKey, cached, TimeSpan.FromMinutes(10));
            }

            return cached;
        }

        // ======================================================
        // 🔹 GET BY USER + COURSE (for duplicate validation)
        // ======================================================
        /// <summary>
        /// Retrieves a specific enrollment that matches both User ID and Course ID.
        /// Used mainly for validation (e.g., preventing duplicate enrollments).
        /// </summary>
        public async Task<EnrollmentDetailDto?> GetByUserAndCourseAsync(Guid userId, Guid courseId)
        {
            var cacheKey = $"{CacheKeys.EnrollmentsByUser(userId)}_{courseId}";
            if (!_cache.TryGetValue(cacheKey, out EnrollmentDetailDto? cached))
            {
                var enrollment = await _repo.GetByUserAndCourseAsync(userId, courseId);
                if (enrollment == null) return null;

                cached = _mapper.Map<EnrollmentDetailDto>(enrollment);
                _cache.Set(cacheKey, cached, TimeSpan.FromMinutes(10));
            }

            return cached;
        }

        // ======================================================
        // 🔹 GET BY USER ID
        // ======================================================
        /// <summary>
        /// Retrieves all enrollments for a specific user (based on User ID).
        /// Includes related Course and Program entities.
        /// </summary>
        public async Task<IEnumerable<EnrollmentListDto>> GetByUserIdAsync(Guid userId)
        {
            var cacheKey = CacheKeys.EnrollmentsByUser(userId);
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<EnrollmentListDto>? cachedList))
            {
                var enrollments = await _repo.GetByUserIdAsync(userId);
                cachedList = _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
                _cache.Set(cacheKey, cachedList, TimeSpan.FromMinutes(10));
            }

            return cachedList!;
        }

        // ======================================================
        // 🔹 GET BY COURSE ID
        // ======================================================
        /// <summary>
        /// Retrieves all enrollments that belong to a specific Course ID.
        /// Typically used in course detail views or reports.
        /// </summary>
        public async Task<IEnumerable<EnrollmentListDto>> GetByCourseIdAsync(Guid courseId)
        {
            var cacheKey = CacheKeys.EnrollmentsByCourse(courseId);
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<EnrollmentListDto>? cachedList))
            {
                var enrollments = await _repo.GetByCourseIdAsync(courseId);
                cachedList = _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
                _cache.Set(cacheKey, cachedList, TimeSpan.FromMinutes(10));
            }

            return cachedList!;
        }

        // ======================================================
        // 🔹 GET BY COURSE NAME 
        // ======================================================
        /// <summary>
        /// Retrieves all enrollments for a specific course name.
        /// Performs a case-insensitive search and caches results for 10 minutes.
        /// </summary>
        /// <param name="courseName">The course name to search by.</param>
        /// <returns>List of enrollments for that course name.</returns>
        public async Task<IEnumerable<EnrollmentListDto>> GetByCourseNameAsync(string courseName)
        {
            var cacheKey = $"enrollments_course_{courseName.ToLower()}";

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<EnrollmentListDto>? cachedList))
            {
                var enrollments = await _repo.GetByCourseNameAsync(courseName);
                cachedList = _mapper.Map<IEnumerable<EnrollmentListDto>>(enrollments);
                _cache.Set(cacheKey, cachedList, TimeSpan.FromMinutes(10));
            }

            return cachedList!;
        }

        // ======================================================
        // 🔹 CREATE ENROLLMENT
        // ======================================================
        /// <summary>
        /// Creates a new enrollment for a user in a specific course.
        /// Validates against duplicate enrollments before saving.
        /// </summary>
        public async Task<EnrollmentDetailDto> CreateAsync(CreateEnrollmentDto dto)
        {
            var existing = await _repo.GetByUserAndCourseAsync(dto.UserId, dto.CourseId);
            if (existing != null)
                throw new InvalidOperationException("User is already enrolled in this course.");

            var enrollment = _mapper.Map<Enrollment>(dto);
            await _repo.AddAsync(enrollment);
            await _repo.SaveAsync();

            // Reload with related data
            enrollment = await _repo.GetByIdWithRelationsAsync(enrollment.EnrollmentId);

            // Invalidate cache
            _cache.Remove(CacheKeys.AllEnrollments);
            _cache.Remove(CacheKeys.EnrollmentsByUser(dto.UserId));
            _cache.Remove(CacheKeys.EnrollmentsByCourse(dto.CourseId));

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        // ======================================================
        // 🔹 UPDATE ENROLLMENT
        // ======================================================
        /// <summary>
        /// Updates an existing enrollment (e.g., status, grade, or reason for status change).
        /// Invalidates all related cache keys.
        /// </summary>
        public async Task<EnrollmentDetailDto?> UpdateAsync(Guid enrollmentId, UpdateEnrollmentDto dto)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return null;

            _mapper.Map(dto, enrollment);
            _repo.Update(enrollment);
            await _repo.SaveAsync();

            // Invalidate affected caches
            _cache.Remove(CacheKeys.AllEnrollments);
            _cache.Remove(CacheKeys.EnrollmentsByUser(enrollment.UserId));
            _cache.Remove(CacheKeys.EnrollmentsByCourse(enrollment.CourseId));

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        // ======================================================
        // 🔹 DELETE ENROLLMENT
        // ======================================================
        /// <summary>
        /// Deletes an enrollment by ID.
        /// Removes it from the database and clears relevant cache entries.
        /// </summary>
        public async Task<bool> DeleteAsync(Guid enrollmentId)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return false;

            _repo.Delete(enrollment);
            await _repo.SaveAsync();

            // Invalidate related caches
            _cache.Remove(CacheKeys.AllEnrollments);
            _cache.Remove(CacheKeys.EnrollmentsByUser(enrollment.UserId));
            _cache.Remove(CacheKeys.EnrollmentsByCourse(enrollment.CourseId));

            return true;
        }
    }
}
