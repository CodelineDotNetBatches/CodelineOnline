
namespace ReportsManagements.Services
{
    public interface IReasonCodeService
    {
        Task<bool> DeactivateAsync(int id);
    }
}