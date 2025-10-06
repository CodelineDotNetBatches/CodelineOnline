using CoursesManagement.Models;
using CoursesManagement.Repos;
using CoursesManagement;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CoursesManagement.Repos
{
    public class ProgramsRepo : GenericRepo<Programs>, IProgramsRepo
    {
        private readonly CoursesDbContext _context;
        public ProgramsRepo(CoursesDbContext context) : base(context)
        {
            _context = context;
        }

        // Program-specific queries:
        // Get program with full course/category hierarchy
        public async Task<Programs?> GetProgramWithCoursesAsync(Guid programId)
        {
            return await _context.Programs
                .Include(p => p.Categories)   
                    .ThenInclude(c => c.Courses)
                .Include(p => p.Courses)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }

        

        // Finds a Program by Guid primary key.

        public async Task<Programs?> GetByIdAsync(Guid id)
        {
            return _context.Programs
                .Include(p => p.Categories)
                .Include(p => p.Courses)
                .FirstOrDefault(p => p.ProgramId == id);
        }


        // Finds a Program by its unique name

        public async Task<Programs?> GetByNameAsync(string programName)
        {
            if (string.IsNullOrWhiteSpace(programName)) return null;

            string normalized = programName.Trim().ToLower();
            return await _context.Programs.FirstOrDefaultAsync(p => p.ProgramName.ToLower() == normalized);
        }


        // Checks if a Program name exists (case-insensitive). Optionally exclude a specific ProgramId.

        public async Task<bool> ExistsByNameAsync(string programName, Guid? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(programName)) return false;

            string normalized = programName.Trim().ToLower();
            var query = _context.Programs.AsQueryable();

            if (excludeId .HasValue)
                query = query.Where(p => p.ProgramId != excludeId.Value);

            return await query.AnyAsync(p => p.ProgramName.ToLower() == normalized);
        }




        // gets all programs with their related categories and courses
        public async Task<List<Programs>> GetAllWithDetailsAsync()
        {
            return await _context.Programs
                .Include(p => p.Categories)
                    .ThenInclude(c => c.Courses)
                .Include(p => p.Courses)
                .ToListAsync();

        }

        // Get Program With Categories
        public async Task<Programs?> GetProgramWithCategoriesAsync(Guid programId)
        {
            return await _context.Programs
                .Include(p => p.Categories)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }

        // Get Program With Courses
        public async Task<Programs?> GetProgramWithCoursesOnlyAsync(Guid programId)
        {
            return await _context.Programs
                .Include(p => p.Courses)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
        }

        // Get All Enrolled Students in a Program
        public async Task<List<Enrollment>> GetAllEnrolledStudentsInProgramAsync(Guid programId, List<Enrollment> User)
        {
            var program = await _context.Programs
                .Include(p => p.Courses)
                    .ThenInclude(c => c.Enrollments)
                        .ThenInclude(e => e.UserId)
                .FirstOrDefaultAsync(p => p.ProgramId == programId);
            if (program == null) return new List<Enrollment>();
            var students = program.Courses
                .SelectMany(c => c.Enrollments)
                .Select(e => e.UserId)
                .Distinct()
                .ToList();
            return User;
        }

    }
}
