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
    /// Handles CRUD operations, relationships, and in-memory caching.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        private readonly IProgramsRepo _programRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        // Cache keys
        private const string CacheKey_AllCategories = "Cache_Categories_All";

        public CategoryService(
            ICategoryRepo repo,
            IProgramsRepo programRepo,
            IMapper mapper,
            IMemoryCache cache)
        {
            _repo = repo;
            _programRepo = programRepo;
            _mapper = mapper;
            _cache = cache;
        }

        // =========================================================
        // GET ALL (Cached for 5 minutes)
        // =========================================================
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            if (!_cache.TryGetValue(CacheKey_AllCategories, out IEnumerable<CategoryDto>? cachedCategories))
            {
                var categories = await _repo.GetQueryable()
                    .AsNoTracking()
                    .Include(c => c.Programs)
                    .Include(c => c.Courses)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();

                cachedCategories = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                // Cache the result for 5 minutes
                _cache.Set(CacheKey_AllCategories, cachedCategories, TimeSpan.FromMinutes(5));
            }

            return cachedCategories!;
        }

        // =========================================================
        // GET BY ID (Cached per category for 10 minutes)
        // =========================================================
        public async Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id)
        {
            string cacheKey = $"Cache_Category_{id}";

            if (!_cache.TryGetValue(cacheKey, out CategoryDetailDto? cachedCategory))
            {
                var category = await _repo.GetCategoryFullAsync(id);
                cachedCategory = _mapper.Map<CategoryDetailDto?>(category);

                if (cachedCategory != null)
                    _cache.Set(cacheKey, cachedCategory, TimeSpan.FromMinutes(10));
            }

            return cachedCategory;
        }

        // =========================================================
        // GET BY NAME (Cached per category name)
        // =========================================================
        public async Task<CategoryDetailDto?> GetCategoryByNameAsync(string name)
        {
            string cacheKey = $"Cache_Category_Name_{name.ToLower()}";

            if (!_cache.TryGetValue(cacheKey, out CategoryDetailDto? cachedCategory))
            {
                var category = await _repo.GetByNameAsync(name);
                cachedCategory = _mapper.Map<CategoryDetailDto?>(category);

                if (cachedCategory != null)
                    _cache.Set(cacheKey, cachedCategory, TimeSpan.FromMinutes(10));
            }

            return cachedCategory;
        }

        // =========================================================
        // GET COURSES BY CATEGORY
        // =========================================================
        public async Task<IEnumerable<CourseListDto>> GetCoursesByCategoryAsync(Guid categoryId)
        {
            var category = await _repo.GetCategoryWithCoursesAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<IEnumerable<CourseListDto>>(category.Courses);
        }

        // =========================================================
        // GET PROGRAMS BY CATEGORY
        // =========================================================
        public async Task<IEnumerable<ProgramDetailsDto>> GetProgramsByCategoryAsync(Guid categoryId)
        {
            var category = await _repo.GetCategoryWithProgramsAsync(categoryId);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<IEnumerable<ProgramDetailsDto>>(category.Programs);
        }

        // =========================================================
        // CREATE (Invalidates cache)
        // =========================================================
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            // Attach Programs (Many-to-Many)
            if (dto.ProgramIds != null && dto.ProgramIds.Any())
            {
                var programs = await _programRepo.GetQueryable()
                    .Where(p => dto.ProgramIds.Contains(p.ProgramId))
                    .ToListAsync();
                category.Programs = programs;
            }

            await _repo.AddAsync(category);
            await _repo.SaveAsync();

            // Clear caches
            _cache.Remove(CacheKey_AllCategories);

            return _mapper.Map<CategoryDto>(category);
        }

        // =========================================================
        // UPDATE (Invalidates cache)
        // =========================================================
        public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _repo.GetCategoryFullAsync(id);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(dto, category);

            // Replace Programs
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

            // Clear caches for this category and all list
            _cache.Remove(CacheKey_AllCategories);
            _cache.Remove($"Cache_Category_{id}");
            _cache.Remove($"Cache_Category_Name_{category.CategoryName.ToLower()}");
        }

        // =========================================================
        // DELETE (Invalidates cache)
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

            // Clear caches
            _cache.Remove(CacheKey_AllCategories);
            _cache.Remove($"Cache_Category_{id}");
            _cache.Remove($"Cache_Category_Name_{category.CategoryName.ToLower()}");
        }
    }
}
