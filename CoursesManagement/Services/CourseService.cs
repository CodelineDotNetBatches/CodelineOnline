using AutoMapper;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepo _courseRepo;
        private readonly IMapper _mapper;

        public CourseService(ICourseRepo courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await _courseRepo.GetAll().ToListAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(Guid id)
        {
            return await _courseRepo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Course>> GetCoursesByLevelAsync(LevelType level)
        {
            var query = await _courseRepo.GetCoursesByLevelAsync(level);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByCategoryAsync(int categoryId)
        {
            var query = await _courseRepo.GetCoursesByCategoryAsync(categoryId);
            return await query.ToListAsync();
        }

        public async Task<Course?> GetCourseWithCategoryAsync(Guid id)
        {
            return await _courseRepo.GetCourseWithCategoryAsync(id);
        }

        // --- Create using DTO ---
        public async Task<Course> AddCourseAsync(CourseCreateDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            course.CourseId = Guid.NewGuid(); // ensure a new Guid is assigned
            course.CreatedAt = DateTime.UtcNow;

            await _courseRepo.AddAsync(course);
            await _courseRepo.SaveAsync();

            return course;
        }

        // --- Update using DTO ---
        public async Task<Course?> UpdateCourseAsync(CourseUpdateDto dto)
        {
            var existing = await _courseRepo.GetByIdAsync(dto.CourseId);
            if (existing == null)
                return null;

            _mapper.Map(dto, existing);
            _courseRepo.Update(existing);
            await _courseRepo.SaveAsync();

            return existing;
        }

        public async Task DeleteCourseAsync(Guid id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null)
                throw new KeyNotFoundException("Course not found.");

            _courseRepo.Delete(course);
            await _courseRepo.SaveAsync();
        }
    }
}
