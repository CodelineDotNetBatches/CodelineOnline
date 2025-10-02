using CoursesManagement.Models;
using CoursesManagement.Repos;
using CoursesManagement;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    public class ProgramsRepo : GenericRepo<Program>, IProgramsRepo
    {
        private readonly CoursesDbContext _context;
        public ProgramsRepo(CoursesDbContext context) : base(context)
        {
            _context = context;
        }

        // Program-specific queries:
        public async Task<IEnumerable<Program>> GetProgramsByLevelAsync(LevelType level)
        {
            return await _context.Programs
                .Where(p => p.ProgramLevel == level)
                .ToListAsync();
        }
        public async Task<IEnumerable<Program>> GetProgramsByCategoryAsync(int categoryId)
        {
            return await _context.Programs
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<Program?> GetProgramWithCategoryAsync(Guid programId)
        {
            return await _context.Programs
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }


    }
}
