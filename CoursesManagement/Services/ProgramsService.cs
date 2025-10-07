using AutoMapper;
using CoursesManagement.Caching;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CoursesManagement.Services
{
    public class ProgramsService : IProgramsService
    {
        private readonly IProgramsRepo _repo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProgramsService(IProgramsRepo repo, ICategoryRepo categoryRepo, IMapper mapper, IMemoryCache cache)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _cache = cache;
        }

        // ======================
        // GET ALL (with caching)
        // ======================
        public async Task<IEnumerable<ProgramDetailsDto>> GetAllProgramsAsync()
        {
            if (!_cache.TryGetValue(CacheKeys.AllPrograms, out IEnumerable<ProgramDetailsDto>? cachedPrograms))
            {
                var programs = await _repo.GetQueryable()
                    .AsNoTracking()
                    .ToListAsync();

                cachedPrograms = _mapper.Map<IEnumerable<ProgramDetailsDto>>(programs);
                _cache.Set(CacheKeys.AllPrograms, cachedPrograms, TimeSpan.FromMinutes(10));
            }

            return cachedPrograms!;
        }

        // ======================
        // GET BY ID (with caching)
        // ======================
        public async Task<ProgramDetailsDto?> GetProgramByIdAsync(Guid id)
        {
            var cacheKey = CacheKeys.Program(id);

            if (!_cache.TryGetValue(cacheKey, out ProgramDetailsDto? cachedProgram))
            {
                var program = await _repo.GetQueryable()
                    .Include(p => p.Categories)
                    .Include(p => p.Courses)
                    .FirstOrDefaultAsync(p => p.ProgramId == id);

                cachedProgram = _mapper.Map<ProgramDetailsDto?>(program);

                if (cachedProgram != null)
                    _cache.Set(cacheKey, cachedProgram, TimeSpan.FromMinutes(10));
            }

            return cachedProgram;
        }

        // ======================
        // CREATE
        // ======================
        public async Task<ProgramDetailsDto> CreateProgramAsync(ProgramCreateDto dto)
        {
            var program = _mapper.Map<Programs>(dto);

            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                var categories = await _categoryRepo.GetQueryable()
                    .Where(c => dto.CategoryIds.Contains(c.CategoryId))
                    .ToListAsync();

                program.Categories = categories;
            }

            await _repo.AddAsync(program);
            await _repo.SaveAsync();

            // Invalidate caches
            _cache.Remove(CacheKeys.AllPrograms);

            return _mapper.Map<ProgramDetailsDto>(program);
        }

        // ======================
        // UPDATE
        // ======================
        public async Task UpdateProgramAsync(Guid id, ProgramUpdateDto dto)
        {
            var program = await _repo.GetQueryable()
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.ProgramId == id);

            if (program == null)
                throw new KeyNotFoundException("Program not found.");

            _mapper.Map(dto, program);

            if (dto.CategoryId != null && dto.CategoryId.Any())
            {
                var categories = await _categoryRepo.GetQueryable()
                    .Where(c => dto.CategoryId.Contains(c.CategoryId))
                    .ToListAsync();

                program.Categories.Clear();
                foreach (var cat in categories)
                    program.Categories.Add(cat);
            }

            _repo.Update(program);
            await _repo.SaveAsync();

            // Invalidate caches
            _cache.Remove(CacheKeys.Program(id));
            _cache.Remove(CacheKeys.AllPrograms);
        }

        // ======================
        // DELETE
        // ======================
        public async Task DeleteProgramAsync(Guid id)
        {
            var program = await _repo.GetQueryable()
                .Include(p => p.Courses)
                .FirstOrDefaultAsync(p => p.ProgramId == id);

            if (program == null)
                throw new KeyNotFoundException("Program not found.");

            if (program.Courses != null && program.Courses.Any())
                throw new InvalidOperationException("Cannot delete a program that has courses.");

            _repo.Delete(program);
            await _repo.SaveAsync();

            // Invalidate caches
            _cache.Remove(CacheKeys.Program(id));
            _cache.Remove(CacheKeys.AllPrograms);
        }

        // ======================
        // Get Program By Program Name
        public async Task<ProgramDetailsDto?> GetProgramByNameAsync(string programName)
        {
            if (string.IsNullOrWhiteSpace(programName))
                return null;

            string normalized = programName.Trim().ToLower();

            if (!_cache.TryGetValue(normalized, out ProgramDetailsDto? cachedProgram))
            {
                var program = await _repo.GetQueryable()
                    .Include(p => p.Categories)
                    .Include(p => p.Courses)
                    .FirstOrDefaultAsync(p => p.ProgramName.ToLower() == normalized);

                cachedProgram = _mapper.Map<ProgramDetailsDto?>(program);

                if (cachedProgram != null)
                    _cache.Set(normalized, cachedProgram, TimeSpan.FromMinutes(10));
            }

            return cachedProgram;

        }

        // ======================
        // Get Program with Courses
        public async Task<ProgramDetailsDto?> GetProgramWithCoursesAsync(Guid programId)
        {
            var entity = await _repo.GetProgramWithCoursesAsync(programId);
            return _mapper.Map<ProgramDetailsDto?>(entity);
        }

        // ======================

        // Get Program with Categories

        public async Task<ProgramDetailsDto?> GetProgramWithCategoriesAsync(Guid programId)
        {
            var entity = await _repo.GetProgramWithCategoriesAsync(programId);
            return _mapper.Map<ProgramDetailsDto?>(entity);
        }

    
    }
}
