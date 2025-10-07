using AutoMapper;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

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
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public CategoryService(ICategoryRepo repo, IProgramsRepo programRepo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _programRepo = programRepo;
            _mapper = mapper;
            _cache = cache;

            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }

        // =========================================================
        // GET ALL
        // =========================================================
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            const string cacheKey = "AllCategories";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<CategoryDto>? cached))
                return cached!;

            var categories = await _repo.GetQueryable()
                .AsNoTracking()
                .Include(c => c.Programs)
                .Include(c => c.Courses)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            var mapped = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            _cache.Set(cacheKey, mapped, _cacheOptions);
            return mapped;
        }

        // =========================================================
        // GET CATEGORY (by Id OR Name)
        // =========================================================
        public async Task<CategoryDetailDto?> GetCategoryAsync(Guid? id = null, string? name = null)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Either Category ID or Name must be provided.");

            string cacheKey = id != null && id != Guid.Empty
                ? $"Category_{id}"
                : $"Category_Name_{name!.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out CategoryDetailDto? cached))
                return cached!;

            Category? category;

            if (id != null && id != Guid.Empty)
                category = await _repo.GetCategoryFullAsync(id.Value);
            else
                category = await _repo.GetByNameAsync(name!);

            var mapped = _mapper.Map<CategoryDetailDto?>(category);

            if (mapped != null)
                _cache.Set(cacheKey, mapped, _cacheOptions);

            return mapped;
        }

        // =========================================================
        // GET COURSES BY CATEGORY (ID or Name)
        // =========================================================
        public async Task<IEnumerable<CourseListDto>> GetCoursesByCategoryAsync(Guid? id = null, string? name = null)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Either Category ID or Name must be provided.");

            string cacheKey = id != null && id != Guid.Empty
                ? $"CategoryCourses_{id}"
                : $"CategoryCourses_Name_{name!.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<CourseListDto>? cached))
                return cached!;

            Category? category;

            if (id != null && id != Guid.Empty)
                category = await _repo.GetCategoryWithCoursesAsync(id.Value);
            else
                category = await _repo.GetByNameAsync(name!);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            var mapped = _mapper.Map<IEnumerable<CourseListDto>>(category.Courses);
            _cache.Set(cacheKey, mapped, _cacheOptions);

            return mapped;
        }

        // =========================================================
        // GET PROGRAMS BY CATEGORY (ID or Name)
        // =========================================================
        public async Task<IEnumerable<ProgramDetailsDto>> GetProgramsByCategoryAsync(Guid? id = null, string? name = null)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Either Category ID or Name must be provided.");

            string cacheKey = id != null && id != Guid.Empty
                ? $"CategoryPrograms_{id}"
                : $"CategoryPrograms_Name_{name!.ToLower()}";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<ProgramDetailsDto>? cached))
                return cached!;

            Category? category;

            if (id != null && id != Guid.Empty)
                category = await _repo.GetCategoryWithProgramsAsync(id.Value);
            else
                category = await _repo.GetByNameAsync(name!);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            var mapped = _mapper.Map<IEnumerable<ProgramDetailsDto>>(category.Programs);
            _cache.Set(cacheKey, mapped, _cacheOptions);

            return mapped;
        }

        // =========================================================
        // CREATE
        // =========================================================
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            // Attach selected programs (if provided)
            if (dto.ProgramIds != null && dto.ProgramIds.Any())
            {
                var programs = await _programRepo.GetQueryable()
                    .Where(p => dto.ProgramIds.Contains(p.ProgramId))
                    .ToListAsync();

                category.Programs = programs;
            }

            //  Save new category to the database
            await _repo.AddAsync(category);
            await _repo.SaveAsync();

            //  Clear cache (so next GetAll fetches updated data)
            _cache.Remove("AllCategories");

            //  Reload category with related programs to show full list in response
            var created = await _repo.GetCategoryFullAsync(category.CategoryId);

            return _mapper.Map<CategoryDto>(created);
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

            // Invalidate cache for that category
            _cache.Remove("AllCategories");
            _cache.Remove($"Category_{id}");
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

            // Invalidate cache
            _cache.Remove("AllCategories");
            _cache.Remove($"Category_{id}");
        }
    }
}