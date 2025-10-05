using AutoMapper;
using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Repos;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Services
{
    // Business logic layer for Programs.
    public class ProgramsService : IProgramsService
    {
        private readonly IProgramsRepo _repo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public ProgramsService(IProgramsRepo repo, ICategoryRepo categoryRepo, IMapper mapper)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }
        // GET ALL

        public async Task<IEnumerable<ProgramDetailsDto>> GetAllProgramsAsync()
        {
            var programs = await _repo.GetQueryable()
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<IEnumerable<ProgramDetailsDto>>(programs);
        }


        // GET BY ID (with relations)

        public async Task<ProgramDetailsDto?> GetProgramByIdAsync(Guid id)
        {
            var program = await _repo.GetQueryable()
                .Include(p => p.Categories) // M:M relation
                .Include(p => p.Courses)    // 1:M (or existing navigation) relation
                .FirstOrDefaultAsync(p => p.ProgramId == id);

            return _mapper.Map<ProgramDetailsDto?>(program);
        }


        // CREATE

        public async Task<ProgramDetailsDto> CreateProgramAsync(ProgramCreateDto dto)
        {
            var program = _mapper.Map<Programs>(dto);

            // Attach M:M Categories if provided
            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                var categories = await _categoryRepo.GetQueryable()
                    .Where(c => dto.CategoryIds.Contains(c.CategoryId))
                    .ToListAsync();

                program.Categories = categories;
            }

            await _repo.AddAsync(program);
            await _repo.SaveAsync();

            return _mapper.Map<ProgramDetailsDto>(program);
        }


        // UPDATE

        public async Task UpdateProgramAsync(Guid id, ProgramUpdateDto dto)
        {
            var program = await _repo.GetQueryable()
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.ProgramId == id);

            if (program == null)
                throw new KeyNotFoundException("Program not found.");

            // Update scalar properties
            _mapper.Map(dto, program);

            // Replace M:M Categories if provided
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
        }


        // DELETE

        public async Task DeleteProgramAsync(Guid id)
        {
            var program = await _repo.GetQueryable()
                .Include(p => p.Courses)
                .FirstOrDefaultAsync(p => p.ProgramId == id);

            if (program == null)
                throw new KeyNotFoundException("Program not found.");

            // Prevent deleting a program that still has courses
            if (program.Courses != null && program.Courses.Any())
                throw new InvalidOperationException("Cannot delete a program that has courses.");

            _repo.Delete(program);
            await _repo.SaveAsync();
        }
    }
}
