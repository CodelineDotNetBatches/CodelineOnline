using CoursesManagement.Models;
using CoursesManagement.Repos;
using CoursesManagement;
using Microsoft.EntityFrameworkCore;
namespace CoursesManagement.Repos
{
    public class EnrollmentRepo : GenericRepo<Enrollment>, IEnrollmentRepo
    {
        private readonly CoursesDbContext _context;
        public EnrollmentRepo(CoursesDbContext context) : base(context)
        {
            _context = context;
        }
        // Enrollment-specific queries:
        //to GetEnrollmentsByTrainee ...
        public async Task<IEnumerable<Enrollment>> GetEnrollmentsByTraineeAsync(int traineeId)
        {
            return await _context.Set<Enrollment>()
                .Where(e => e.TraineeId == traineeId)
                .Include(e => e.Course) // Include related Course data
                .Include(e => e.Program) // Include related Program data
                .ToListAsync();
        }
        //to GetAllEnrollmentByCourseId ...
        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentByCourseId(int courseId)
        {
            return await _context.Set<Enrollment>()
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Course) // Include related Course data
                .Include(e => e.Program) // Include related Program data
                .ToListAsync();
        }
        //to GetAllEnrollmentByProgramId ...
        public async Task<IEnumerable<Enrollment>> GetAllEnrollmentByProgramId(int programId)
        {
            return await _context.Set<Enrollment>()
                .Where(e => e.ProgramId == programId)
                .Include(e => e.Course) // Include related Course data
                .Include(e => e.Program) // Include related Program data
                .ToListAsync();
        }
    }
}
