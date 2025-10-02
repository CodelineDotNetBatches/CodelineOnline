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
       
        public async Task<Programs?> GetProgramWithCoursesAsync(Guid programId)
        {
            return await _context.Programs
                .Include(p => p.Categories)   // Example navigation
                .ThenInclude(c => c.Courses)  // Drill deeper
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }


    }
}
