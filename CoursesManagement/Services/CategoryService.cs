using AutoMapper;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Services
{
    /// <summary>
    /// Business logic layer for Category.
    /// Applies rules, mappings, and optimizations.
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

        // ======================
        // GET ALL
        // ======================
        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            // Avoid N+1 problem → AsNoTracking() for performance
            var categories = await _repo.GetQueryable()
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        // ======================
        // GET BY ID
        // ======================
        public async Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id)
        {
            var category = await _repo.GetQueryable()
                .Include(c => c.Programs)  // eager load M:M
                .Include(c => c.Courses)   // eager load 1:M
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            return _mapper.Map<CategoryDetailDto?>(category);
        }

        // ======================
        // CREATE
        // ======================
        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto dto)
        {
            // Map base properties
            var category = _mapper.Map<Category>(dto);

            // Attach M:M Programs
            if (dto.ProgramIds.Any())
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

            // Map simple props
            _mapper.Map(dto, category);

            // Update Programs (replace M:M)
            if (dto.ProgramIds.Any())
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
        }
    }
}
