using ReportsManagements.Models;
using System.Threading.Tasks;

namespace ReportsManagements.Services
{
    public interface IAttendanceRecordService
    {
        // Create new attendance record
        Task<AttendanceRecord> CreateAsync(AttendanceRecord record);
        Task<AttendanceRecord?> GetByIdAsync(int id);
        Task<IEnumerable<AttendanceRecord>> GetFilteredAsync(int? studentId,
          int? sessionId,
          DateTime? fromDate,
          DateTime? toDate,
          string? reviewStatus);
       
        Task<AttendanceRecord?> UpdateAsync(int id, AttendanceRecord updated);
        Task<bool> DeleteAsync(int id);

        Task<AttendanceRecord?> CheckoutAsync(int id, DateTime checkout);
        Task<AttendanceRecord?> ReviewAsync(int id, string reviewStatus, string reviewedBy);
        IQueryable<AttendanceRecord> GetQueryable();
        //SoftDeleteAsync
        Task<bool> SoftDeleteAsync(int id);


    }
}
