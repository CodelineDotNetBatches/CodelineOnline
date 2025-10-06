using AutoMapper;
using CoursesManagement.Caching;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoursesManagement.Services
{
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

        // ======================
        // GET ALL (with caching)
        // ======================
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

        // ======================
        // GET BY ID (with caching)
        // ======================
        public async Task<EnrollmentDetailDto?> GetByIdAsync(Guid enrollmentId)
        {
            var cacheKey = CacheKeys.EnrollmentsByCourse(enrollmentId);
            if (!_cache.TryGetValue(cacheKey, out EnrollmentDetailDto? cached))
            {
                var enrollment = await _repo.GetByIdAsync(enrollmentId);
                if (enrollment == null) return null;

                // Load related entities (from repo)
                enrollment = await _repo.GetByIdWithRelationsAsync(enrollmentId);

                cached = _mapper.Map<EnrollmentDetailDto>(enrollment);
                _cache.Set(cacheKey, cached, TimeSpan.FromMinutes(10));
            }

            return cached;
        }

        // ======================
        // GET BY USER & COURSE
        // ======================
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

        // ======================
        // GET BY USER ID
        // ======================
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

        // ======================
        // GET BY COURSE ID
        // ======================
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

        // ======================
        // CREATE
        // ======================
        public async Task<EnrollmentDetailDto> CreateAsync(CreateEnrollmentDto dto)
        {
            var existing = await _repo.GetByUserAndCourseAsync(dto.UserId, dto.CourseId);
            if (existing != null)
                throw new InvalidOperationException("User is already enrolled in this course.");

            var enrollment = _mapper.Map<Enrollment>(dto);
            await _repo.AddAsync(enrollment);
            await _repo.SaveAsync();

            // Reload with related entities
            enrollment = await _repo.GetByIdWithRelationsAsync(enrollment.EnrollmentId);

            // Invalidate affected cache groups
            _cache.Remove(CacheKeys.AllEnrollments);
            _cache.Remove(CacheKeys.EnrollmentsByUser(dto.UserId));
            _cache.Remove(CacheKeys.EnrollmentsByCourse(dto.CourseId));

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        // ======================
        // UPDATE
        // ======================
        public async Task<EnrollmentDetailDto?> UpdateAsync(Guid enrollmentId, UpdateEnrollmentDto dto)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return null;

            _mapper.Map(dto, enrollment);
            _repo.Update(enrollment);
            await _repo.SaveAsync();

            _cache.Remove(CacheKeys.AllEnrollments);
            _cache.Remove(CacheKeys.EnrollmentsByUser(enrollment.UserId));
            _cache.Remove(CacheKeys.EnrollmentsByCourse(enrollment.CourseId));

            return _mapper.Map<EnrollmentDetailDto>(enrollment);
        }

        // ======================
        // DELETE
        // ======================
        public async Task<bool> DeleteAsync(Guid enrollmentId)
        {
            var enrollment = await _repo.GetByIdAsync(enrollmentId);
            if (enrollment == null) return false;

            _repo.Delete(enrollment);
            await _repo.SaveAsync();

            _cache.Remove(CacheKeys.AllEnrollments);
            _cache.Remove(CacheKeys.EnrollmentsByUser(enrollment.UserId));
            _cache.Remove(CacheKeys.EnrollmentsByCourse(enrollment.CourseId));

            return true;
        }
    }
}
