using ReportsManagements.Repositories;

namespace ReportsManagements.Services
{
    public class ReasonCodeService : IReasonCodeService
    {
        // Repositories via dependency injection
        private readonly IReasonCodeRepository _repo;
        public ReasonCodeService(IReasonCodeRepository repo) => _repo = repo;
        public async Task<bool> DeactivateAsync(int id)
        {
            var code = await _repo.GetByIdAsync(id);
            if (code == null) return false;
            code.IsActive = false;
            await _repo.UpdateAsync(code);
            return true;
        }
    }
}
