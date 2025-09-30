using ReportsManagements.Models;

namespace ReportsManagements.Services
{
    public interface IAttendanceRecordService
    {

        Task<AttendanceRecord> CreateAsync(AttendanceRecord record);
        Task<AttendanceRecord?> GetByIdAsync(int id);
        Task<AttendanceRecord?> UpdateAsync(int id, AttendanceRecord updated);
        Task<bool> DeleteAsync(int id);
    }
}
