using CoursesManagement.Models;
using CoursesManagement.Repos;
using CoursesManagement;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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

        // Finds a Program by Guid primary key.

        public async Task<IQueryable<Programs?>> GetByIdAsync(Guid id)
        {
            return _context.Programs.FirstOrDefault(p => p.ProgramId == id);
        }


        // Finds a Program by its unique name

        public async Task<Programs?> GetByNameAsync(string programName)
        {
            if (string.IsNullOrWhiteSpace(programName)) return null;

            string normalized = programName.Trim().ToLower();
            return _context.Programs.FirstOrDefaultAsync(p => p.ProgramName.ToLower() == normalized);
        }


        // Checks if a Program name exists (case-insensitive). Optionally exclude a specific ProgramId.

        public async Task<bool> ExistsByNameAsync(string programName, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(programName)) return false;

            string normalized = programName.Trim().ToLower();
            var query = _dbSet.AsQueryable();

            if (excludeId is Guid idToExclude)
                query = query.Where(p => p.ProgramId != idToExclude);

            return _context.Programs.AnyAsync(p => p.ProgramName.ToLower() == normalized);
        }




        // gets all programs with their related categories and courses
        public async Task<IQueryable<Programs>> GetAllWithDetailsAsync()
        {
            return _context.Programs
                .Include(p => p.Categories)
                .ThenInclude(c => c.Courses);

        }

    }
}
