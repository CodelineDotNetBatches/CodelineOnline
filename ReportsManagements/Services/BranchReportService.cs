using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using AutoMapper;

namespace ReportsManagements.Services
{
    public class BranchReportService : IBranchReportService
    {
        private readonly IBranchReportRepository _repo;
        private readonly IMapper _mapper;

        public BranchReportService(IBranchReportRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<BranchReportDtos.BranchReportResponseDto> CreateReportAsync(BranchReportDtos.BranchReportCreateDto dto)
        {
            var report = _mapper.Map<BranchReport>(dto);
            var created = await _repo.AddAsync(report);
            return _mapper.Map<BranchReportDtos.BranchReportResponseDto>(created);
        }

        public async Task<IEnumerable<BranchReportDtos.BranchReportResponseDto>> GetAllReportsAsync()
        {
            var reports = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<BranchReportDtos.BranchReportResponseDto>>(reports);
        }

        public async Task<BranchReportDtos.BranchReportResponseDto?> GetReportByIdAsync(int id)
        {
            var report = await _repo.GetByIdAsync(id);
            return report == null ? null : _mapper.Map<BranchReportDtos.BranchReportResponseDto>(report);
        }

        public async Task<BranchReportDtos.BranchReportResponseDto?> UpdateReportAsync(int id, BranchReportDtos.BranchReportCreateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);
            var updated = await _repo.UpdateAsync(existing);
            return _mapper.Map<BranchReportDtos.BranchReportResponseDto>(updated);
        }

        public async Task DeleteReportAsync(int id) =>
            await _repo.DeleteAsync(id);

        public async Task<IEnumerable<BranchReportDtos.BranchReportResponseDto>> GetReportsByBranchIdAsync(int branchId)
        {
            var reports = await _repo.GetReportsByBranchIdAsync(branchId);
            return _mapper.Map<IEnumerable<BranchReportDtos.BranchReportResponseDto>>(reports);
        }
    }
}
