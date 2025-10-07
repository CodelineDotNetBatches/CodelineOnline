using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using static ReportsManagements.DTOs.TrainerReportDtos;
namespace ReportsManagements.Services
{
    public class TrainerReportService : ITrainerReportService
    {

        private readonly ITrainerReportRepository _repo;

        public TrainerReportService(ITrainerReportRepository repo)
        {
            _repo = repo;
        }



        public async Task<IEnumerable<TrainerReportResponseDto>> GetAllAsync()
        {
            var reports = await _repo.GetAllAsync();
            return reports.Select(r => new TrainerReportResponseDto
            {
                TrainerReportId = r.TrainerReportId,
                TotalSessions = r.TotalSessions,
                TotalStudents = r.TotalStudents,
                AttendanceRate = r.AttendanceRate,
                TrainerId = r.TrainerId,
                CourseId = r.CourseId
            });
        }

        public async Task<TrainerReportResponseDto?> GetByIdAsync(int id)
        {
            var report = await _repo.GetByIdAsync(id);
            if (report == null) return null;

            return new TrainerReportResponseDto
            {
                TrainerReportId = report.TrainerReportId,
                TotalSessions = report.TotalSessions,
                TotalStudents = report.TotalStudents,
                AttendanceRate = report.AttendanceRate,
                TrainerId = report.TrainerId,
                CourseId = report.CourseId
            };
        }
        public async Task<TrainerReportResponseDto> CreateAsync(TrainerReportCreateDto dto)
        {
            var entity = new TrainerReport
            {
                TotalSessions = dto.TotalSessions,
                TotalStudents = dto.TotalStudents,
                AttendanceRate = dto.AttendanceRate,
                TrainerId = dto.TrainerId,
                CourseId = dto.CourseId
            };

            await _repo.AddAsync(entity);

            return new TrainerReportResponseDto
            {
                TrainerReportId = entity.TrainerReportId,
                TotalSessions = entity.TotalSessions,
                TotalStudents = entity.TotalStudents,
                AttendanceRate = entity.AttendanceRate,
                TrainerId = entity.TrainerId,
                CourseId = entity.CourseId
            };
        }
        public async Task<bool> UpdateAsync(int id, TrainerReportUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            existing.TotalSessions = dto.TotalSessions;
            existing.TotalStudents = dto.TotalStudents;
            existing.AttendanceRate = dto.AttendanceRate;
            existing.TrainerId = dto.TrainerId;
            existing.CourseId = dto.CourseId;

            await _repo.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;

            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
