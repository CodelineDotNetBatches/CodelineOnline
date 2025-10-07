using ReportsManagements.DTOs;

namespace ReportsManagements.Services
{
    public interface ITrainerReportService
    {
        Task<TrainerReportDtos.TrainerReportResponseDto> CreateAsync(TrainerReportDtos.TrainerReportCreateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TrainerReportDtos.TrainerReportResponseDto>> GetAllAsync();
        Task<TrainerReportDtos.TrainerReportResponseDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, TrainerReportDtos.TrainerReportUpdateDto dto);
    }
}