namespace ReportsManagements.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Models.AttendanceRecord?> GetByIdAsync(int id);
        Task<IEnumerable<Models.AttendanceRecord>> GetByStudentAndSessionAsync(int studentId, int sessionId);
        Task AddAsync(Models.AttendanceRecord record);
        Task UpdateAsync(Models.AttendanceRecord record);
        Task DeleteAsync(int id);


    }

}
