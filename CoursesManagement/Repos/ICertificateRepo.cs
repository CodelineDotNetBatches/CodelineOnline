using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface ICertificateRepo
    {
        Task<bool> ExistsByUserAndCourseAsync(int userId, int courseId);
        Task<Certificate?> GetByUrlAsync(string certificateUrl);
        Task<Certificate?> GetByUserAndCourseAsync(int userId, int courseId);
        Task<IQueryable<Certificate>> GetByUserAsync(int userId);
    }
}