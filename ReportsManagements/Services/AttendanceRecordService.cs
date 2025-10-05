using ReportsManagements.Models;
using ReportsManagements.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ReportsManagements.Services
{
    public class AttendanceRecordService : IAttendanceRecordService
    {
        private readonly IAttendanceRepository _repo;

        // Allowed status transitions
        private readonly Dictionary<string, string[]> _allowedTransitions = new()
        {
            { "Pending", new[] { "Present", "Late", "Absent" } }
        };

        public AttendanceRecordService(IAttendanceRepository repo) => _repo = repo;

        // Create new attendance record
        public async Task<AttendanceRecord> CreateAsync(AttendanceRecord record)
        {
            if (record.CheckOut.HasValue && !(record.CheckIn < record.CheckOut.Value))
                throw new ArgumentException("CheckOut must be later than CheckIn.");

            if (!string.IsNullOrEmpty(record.Status) && record.Status != "Pending")
                throw new ArgumentException("Initial status must be 'Pending'.");

            record.Status = "Pending";
            record.ReviewStatus = "Pending";
            record.CreatedAt = DateTime.UtcNow;

            await _repo.AddAsync(record);
            return record;
        }

        public async Task<AttendanceRecord?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<IEnumerable<AttendanceRecord>> GetFilteredAsync(
         int? studentId,
         int? sessionId,
        DateTime? fromDate,
        DateTime? toDate,
        string? reviewStatus)
        {
            var query = _repo.GetQueryable();

            if (studentId.HasValue)
                query = query.Where(a => a.StudentId == studentId.Value);

            if (sessionId.HasValue)
                query = query.Where(a => a.SessionId == sessionId.Value);

            if (fromDate.HasValue)
                query = query.Where(a => a.CheckIn >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(a => a.CheckIn <= toDate.Value);

            if (!string.IsNullOrWhiteSpace(reviewStatus))
                query = query.Where(a => a.ReviewStatus == reviewStatus);

            return await query.ToListAsync();
        }

        // Update attendance record (with validation)
        public async Task<AttendanceRecord?> UpdateAsync(int id, AttendanceRecord updated)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return null;

            // Checkout validation
            if (updated.CheckOut.HasValue && !(existing.CheckIn < updated.CheckOut.Value))
                throw new ArgumentException("CheckOut must be later than CheckIn.");

            // Status transition validation
            if (!string.IsNullOrEmpty(updated.Status) && existing.Status != updated.Status)
            {
                if (!_allowedTransitions.TryGetValue(existing.Status, out var allowed) ||
                    !allowed.Contains(updated.Status))
                {
                    throw new InvalidOperationException(
                        $"Invalid status transition from '{existing.Status}' to '{updated.Status}'."
                    );
                }
                existing.Status = updated.Status;
            }

            // Update other fields
            existing.CheckOut = updated.CheckOut ?? existing.CheckOut;
            existing.ReviewStatus = updated.ReviewStatus ?? existing.ReviewStatus;
            existing.ReasonCodeId = updated.ReasonCodeId ?? existing.ReasonCodeId;
            existing.GeolocationId = updated.GeolocationId != 0 ? updated.GeolocationId : existing.GeolocationId;


            await _repo.UpdateAsync(existing);
            return existing;
        }

        // Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }

        // Checkout endpoint
        public async Task<AttendanceRecord?> CheckoutAsync(int id, DateTime checkout)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            if (!(existing.CheckIn < checkout))
                throw new ArgumentException("CheckOut must be later than CheckIn.");

            existing.CheckOut = checkout;

            // auto mark Present if allowed
            if (existing.Status == "Pending" && _allowedTransitions["Pending"].Contains("Present"))
            {
                existing.Status = "Present";
            }

            await _repo.UpdateAsync(existing);
            return existing;
        }
        public IQueryable<AttendanceRecord> GetQueryable()
        {
            return _repo.GetQueryable();
        }

        // Review endpoint
        public async Task<AttendanceRecord?> ReviewAsync(int id, string reviewStatus, string reviewedBy)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            if (existing.ReviewStatus != "Pending")
                throw new InvalidOperationException("Only Pending records can be reviewed.");

            existing.ReviewStatus = reviewStatus;
            existing.UploadedBy = reviewedBy;
            existing.UploadedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existing);
            return existing;
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {

            var record = await _repo.GetByIdAsync(id);
            if (record == null)
                return false;

            record.IsDeleted = true;
            await _repo.UpdateAsync(record);
            return true;
        }

    }
}
