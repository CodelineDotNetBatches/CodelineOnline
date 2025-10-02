using CoursesManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManagement.Repos
{
    public class CertificateRepo : GenericRepo<Certificate>, ICertificateRepo
    {
        public CertificateRepo(CoursesDbContext context) : base(context) { } // call base constructor

        public async Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId) // custom method
        {
            return await _dbSet // use _dbSet from base class
                .AsNoTracking() // no tracking for read-only
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CourseId == courseId); // lambda expression
        }
        public async Task<bool> ExistsByUserAndCourseAsync(int userId, int courseId) // custom method
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(c => c.UserId == userId && c.CourseId == courseId);
        }
        public async Task<Certificate?> GetByUrlAsync(string certificateUrl) // custom method
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CertificateUrl == certificateUrl);
        }

        public async Task<IQueryable<Certificate>> GetByUserAsync(int userId) // custom method
        {
            return _dbSet
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.IssuedAt);
        }
    }
}
