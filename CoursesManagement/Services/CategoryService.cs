using AutoMapper;
using CoursesManagement.Caching;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoursesManagement.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepo _repo;
        private readonly IProgramsRepo _programRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public CategoryService(ICategoryRepo repo, IProgramsRepo programRepo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _programRepo = programRepo;
            _mapper = mapper;
            _cache = cache;
        }

        // ======================
        // GET ALL (with caching)
        // ======================
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            if (!_cache.TryGetValue(CacheKeys.AllCategories, out IEnumerable<CategoryDto>? cachedCategories))
            {
                var categories = await _repo.GetQueryable()
                    .AsNoTracking()
                    .ToListAsync();

                cachedCategories = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                // Cache for 10 minutes
                _cache.Set(CacheKeys.AllCategories, cachedCategories, TimeSpan.FromMinutes(10));
            }

            return cachedCategories!;
        }

        // ======================
        // GET BY ID (with caching)
        // ======================
        public async Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id)
        {
            var cacheKey = CacheKeys.Category(id);

            if (!_cache.TryGetValue(cacheKey, out CategoryDetailDto? cachedCategory))
            {
                var category = await _repo.GetQueryable()
                    .Include(c => c.Programs)
                    .Include(c => c.Courses)
                    .FirstOrDefaultAsync(c => c.CategoryId == id);

                cachedCategory = _mapper.Map<CategoryDetailDto?>(category);

                if (cachedCategory != null)
                    _cache.Set(cacheKey, cachedCategory, TimeSpan.FromMinutes(10));
            }

            return cachedCategory;
        }

        // ======================
        // CREATE
        // ======================
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);

            if (dto.ProgramIds != null && dto.ProgramIds.Any())
            {
                var programs = await _programRepo.GetQueryable()
                    .Where(p => dto.ProgramIds.Contains(p.ProgramId))
                    .ToListAsync();
                category.Programs = programs;
            }

            await _repo.AddAsync(category);
            await _repo.SaveAsync();

            // Invalidate related caches
            _cache.Remove(CacheKeys.AllCategories);

            return _mapper.Map<CategoryDto>(category);
        }

        // ======================
        // UPDATE
        // ======================
        public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto dto)
        {
            var category = await _repo.GetQueryable()
                .Include(c => c.Programs)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            _mapper.Map(dto, category);

            if (dto.ProgramIds != null && dto.ProgramIds.Any())
            {
                var programs = await _programRepo.GetQueryable()
                    .Where(p => dto.ProgramIds.Contains(p.ProgramId))
                    .ToListAsync();

                category.Programs.Clear();
                foreach (var program in programs)
                    category.Programs.Add(program);
            }

            _repo.Update(category);
            await _repo.SaveAsync();

            // Invalidate related caches
            _cache.Remove(CacheKeys.Category(id));
            _cache.Remove(CacheKeys.AllCategories);
        }

        // ======================
        // DELETE
        // ======================
        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _repo.GetQueryable()
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            if (category.Courses.Any())
                throw new InvalidOperationException("Cannot delete a category that has courses.");

            _repo.Delete(category);
            await _repo.SaveAsync();

            // Invalidate cache
            _cache.Remove(CacheKeys.Category(id));
            _cache.Remove(CacheKeys.AllCategories);
        }
    }
}
