using CoursesManagement.Models;

namespace CoursesManagement.Repos
{
    public interface IEnrollmentRepo
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollmentByCourseId(int courseId);
        Task<IEnumerable<Enrollment>> GetAllEnrollmentByProgramId(int programId);
        Task<IEnumerable<Enrollment>> GetEnrollmentsByTraineeAsync(int traineeId);
    }
}