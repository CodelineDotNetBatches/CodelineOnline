using ReportsManagements.DTOs;

namespace ReportsManagements.Services
{
    public interface IBranchReportService
    {
        Task<BranchReportDtos.BranchReportResponseDto> CreateReportAsync(BranchReportDtos.BranchReportCreateDto dto);
        Task DeleteReportAsync(int id);
        Task<IEnumerable<BranchReportDtos.BranchReportResponseDto>> GetAllReportsAsync();
        Task<BranchReportDtos.BranchReportResponseDto?> GetReportByIdAsync(int id);
        Task<IEnumerable<BranchReportDtos.BranchReportResponseDto>> GetReportsByBranchIdAsync(int branchId);
        Task<BranchReportDtos.BranchReportResponseDto?> UpdateReportAsync(int id, BranchReportDtos.BranchReportCreateDto dto);
    }
}