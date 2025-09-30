using ReportsManagements.Models;
using ReportsManagements.Repositories;

namespace ReportsManagements.Services
{
    public class AttendanceRecordService : IAttendanceRecordService
    {
        private readonly IAttendanceRepository _repo;
        public AttendanceRecordService(IAttendanceRepository repo) => _repo = repo;

        public async Task<AttendanceRecord> CreateAsync(AttendanceRecord record)
        {
            
            record.CreatedAt = DateTime.UtcNow;
            await _repo.AddAsync(record);
            return record;
        }

        public async Task<AttendanceRecord?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<AttendanceRecord?> UpdateAsync(int id, AttendanceRecord updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return null;

            existing.CheckIn = updated.CheckIn;
            existing.CheckOut = updated.CheckOut;
            existing.Status = updated.Status;
            existing.ReviewStatus = updated.ReviewStatus;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
