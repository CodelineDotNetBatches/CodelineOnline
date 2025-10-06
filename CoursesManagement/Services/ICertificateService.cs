using CoursesManagement.DTOs;

namespace CoursesManagement.Services
{
    public interface ICertificateService
    {
        Task DeleteAsync(Guid certificateId);
        Task<CertificateDetailsDto?> GetByIdAsync(Guid certificateId);
        Task<CertificateDetailsDto> IssueAsync(CertificateIssueDto dto);
        Task<List<CertificateListItemDto>> ListByUserAsync(Guid userId);
       // Task<PagedResult<CertificateListItemDto>> SearchAsync(CertificateQueryDto query);
        Task UpdateUrlAsync(Guid certificateId, CertificateUpdateUrlDto dto);
        Task<CertificateVerifyResultDto> VerifyByEnrollmentAsync(Guid enrollmentId);
        Task<CertificateVerifyResultDto> VerifyByUrlAsync(string certificateUrl);
    }
}