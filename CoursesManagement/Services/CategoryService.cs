using AutoMapper;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Business logic layer for Category.
    /// Handles CRUD operations and relationships with Programs & Courses.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        private readonly IProgramsRepo _programRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo repo, IProgramsRepo programRepo, IMapper mapper)
        {
            _repo = repo;
            _programRepo = programRepo;
            _mapper = mapper;
        }

        // =========================================================
        // GET ALL
        // =========================================================
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repo.GetQueryable()
                .AsNoTracking()
                .Include(c => c.Programs)
                .Include(c => c.Courses)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        // =========================================================
        // GET BY ID (Use GetCategoryFullAsync)
        // =========================================================
        public async Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _repo.GetCategoryFullAsync(id);
            return _mapper.Map<CategoryDetailDto?>(category);
        }

        // =========================================================
        // GET BY NAME (Use GetByNameAsync)
        // =========================================================
        public async Task<CategoryDetailDto?> GetCategoryByNameAsync(string name)
        {
            var category = await _repo.GetByNameAsync(name);
            return _mapper.Map<CategoryDetailDto?>(category);
        }

        // =========================================================
        // GET COURSES BY CATEGORY (Uses GetCategoryWithCoursesAsync)
        // =========================================================
        public async Task<IEnumerable<CourseListDto>> GetCoursesByCategoryAsync(Guid categoryId)
        {
            var category = await _repo.GetCategoryWithCoursesAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<IEnumerable<CourseListDto>>(category.Courses);
        }

        // =========================================================
        // GET PROGRAMS BY CATEGORY (Uses GetCategoryWithProgramsAsync)
        // =========================================================
        public async Task<IEnumerable<ProgramDetailsDto>> GetProgramsByCategoryAsync(Guid categoryId)
        {
            var category = await _repo.GetCategoryWithProgramsAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<IEnumerable<ProgramDetailsDto>>(category.Programs);
        }

        // =========================================================
        // CREATE
        // =========================================================
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            // Attach programs (M:M)
            if (dto.ProgramIds != null && dto.ProgramIds.Any())
            {
                var programs = await _programRepo.GetQueryable()
                    .Where(p => dto.ProgramIds.Contains(p.ProgramId))
                    .ToListAsync();

                category.Programs = programs;
            }

            await _repo.AddAsync(category);
            await _repo.SaveAsync();

            return _mapper.Map<CategoryDto>(category);
        }

        // =========================================================
        // UPDATE
        // =========================================================
        public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _repo.GetCategoryFullAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(dto, category);

            // Replace Programs (M:M)
            if (dto.ProgramIds != null && dto.ProgramIds.Any())
            {
                var programs = await _programRepo.GetQueryable()
                    .Where(p => dto.ProgramIds.Contains(p.ProgramId))
                    .ToListAsync();

                category.Programs.Clear();
                foreach (var p in programs)
                    category.Programs.Add(p);
            }

            _repo.Update(category);
            await _repo.SaveAsync();
        }

        // =========================================================
        // DELETE
        // =========================================================
        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _repo.GetCategoryFullAsync(id);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            if (category.Courses.Any())
                throw new InvalidOperationException("Cannot delete a category that has courses.");

            _repo.Delete(category);
            await _repo.SaveAsync();
        }
    }
}
