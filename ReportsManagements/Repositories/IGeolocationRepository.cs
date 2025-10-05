using ReportsManagements.Models;

namespace ReportsManagements.Repositories
{
    public interface IGeolocationRepository
    {
        Task<Geolocation> AddAsync(Geolocation geolocation);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Geolocation>> GetAllAsync();
        Task<Geolocation?> GetByIdAsync(int id);
        Task<Geolocation> UpdateAsync(Geolocation geolocation);
        Task AddAuditAsync(GeoRadiusAudit audit);

        Task<int> GetGeolocationsCountAsync();
        Task<int> GetActiveGeolocationsCountAsync();
    }
}