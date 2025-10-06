using ReportsManagements.Models;

namespace ReportsManagements.Services
{
    public interface IGeolocationService
    {
        Task<Geolocation> CreateAsync(Geolocation geo);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Geolocation>> GetAllAsync();
        Task<Geolocation?> GetByIdAsync(int id);
        Task<Geolocation?> UpdateAsync(Geolocation geo);
        Task<Geolocation?> UpdateRadiusAsync(int id, double newRadius);
    }
}