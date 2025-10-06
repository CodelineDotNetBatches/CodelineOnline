using AutoMapper;
using CoursesManagement.Caching;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoursesManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _courseRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CourseService(ICourseRepo courseRepo, IMapper mapper, IMemoryCache cache)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            _cache = cache;
        }

        // =======================
        // GET ALL (with caching)
        // =======================
        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            if (!_cache.TryGetValue(CacheKeys.AllCourses, out IEnumerable<Course>? cachedCourses))
            {
                cachedCourses = await _courseRepo.GetAll().ToListAsync();
                _cache.Set(CacheKeys.AllCourses, cachedCourses, TimeSpan.FromMinutes(10));
            }
            return cachedCourses!;
        }

        // =======================
        // GET BY ID (with caching)
        // =======================
        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            var cacheKey = CacheKeys.Course(id);

            if (!_cache.TryGetValue(cacheKey, out Course? cachedCourse))
            {
                cachedCourse = await _courseRepo.GetByIdAsync(id);
                if (cachedCourse != null)
                    _cache.Set(cacheKey, cachedCourse, TimeSpan.FromMinutes(10));
            }

            return cachedCourse;
        }

        // =======================
        // GET BY LEVEL (optional cache, transient)
        // =======================
        public async Task<IEnumerable<Course>> GetCoursesByLevelAsync(LevelType level)
        {
            var cacheKey = $"courses_level_{level}";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Course>? cachedCourses))
            {
                var query = await _courseRepo.GetCoursesByLevelAsync(level);
                cachedCourses = await query.ToListAsync();
                _cache.Set(cacheKey, cachedCourses, TimeSpan.FromMinutes(10));
            }
            return cachedCourses!;
        }

        // =======================
        // GET BY CATEGORY (with caching)
        // =======================
        public async Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId)
        {
            var cacheKey = CacheKeys.CoursesByCategory(new Guid(categoryId.ToString()));
            // or adjust depending on your schema (categoryId as int → cast to Guid if applicable)

            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Course>? cachedCourses))
            {
                var query = await _courseRepo.GetCoursesByCategoryAsync(categoryId);
                cachedCourses = await query.ToListAsync();
                _cache.Set(cacheKey, cachedCourses, TimeSpan.FromMinutes(10));
            }

            return cachedCourses!;
        }

        // =======================
        // GET COURSE WITH CATEGORY (with caching)
        // =======================
        public async Task<Course?> GetCourseWithCategoryAsync(Guid id)
        {
            var cacheKey = $"{CacheKeys.Course(id)}_withCategory";

            if (!_cache.TryGetValue(cacheKey, out Course? cachedCourse))
            {
                cachedCourse = await _courseRepo.GetCourseWithCategoryAsync(id);
                if (cachedCourse != null)
                    _cache.Set(cacheKey, cachedCourse, TimeSpan.FromMinutes(10));
            }

            return cachedCourse;
        }

        // =======================
        // CREATE
        // =======================
        public async Task<Course> AddCourseAsync(CourseCreateDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            course.CourseId = Guid.NewGuid();
            course.CreatedAt = DateTime.UtcNow;

            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveAsync();

            // Invalidate caches
            _cache.Remove(CacheKeys.AllCourses);
            if (course.CategoryId != Guid.Empty)
                _cache.Remove(CacheKeys.CoursesByCategory(course.CategoryId));

            return course;
        }

        // =======================
        // UPDATE
        // =======================
        public async Task<Course?> UpdateCourseAsync(CourseUpdateDto dto)
        {
            var existing = await _courseRepo.GetByIdAsync(dto.CourseId);
            if (existing == null)
                return null;

            _mapper.Map(dto, existing);
            _courseRepo.Update(existing);
            await _courseRepo.SaveAsync();

            // Invalidate related caches
            _cache.Remove(CacheKeys.Course(dto.CourseId));
            _cache.Remove(CacheKeys.AllCourses);
            if (existing.CategoryId != Guid.Empty)
                _cache.Remove(CacheKeys.CoursesByCategory(existing.CategoryId));

            return existing;
        }

        // =======================
        // DELETE
        // =======================
        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException("Course not found.");

            _courseRepo.Delete(course);
            await _courseRepo.SaveAsync();

            // Invalidate cache
            _cache.Remove(CacheKeys.Course(id));
            _cache.Remove(CacheKeys.AllCourses);
            if (course.CategoryId != Guid.Empty)
                _cache.Remove(CacheKeys.CoursesByCategory(course.CategoryId));
        }
    }
}
