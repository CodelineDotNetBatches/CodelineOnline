using ReportsManagements.Models;
using ReportsManagements.Repositories;

namespace ReportsManagements.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IGeolocationRepository _repo;

        public GeolocationService(IGeolocationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Geolocation>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<Geolocation?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<Geolocation> CreateAsync(Geolocation geo) =>
            await _repo.AddAsync(geo);

        public async Task<Geolocation?> UpdateAsync(Geolocation geo) =>
            await _repo.UpdateAsync(geo);

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);

        public async Task<Geolocation?> UpdateRadiusAsync(int id, double newRadius)
        {
            var geo = await _repo.GetByIdAsync(id);
            if (geo == null) return null;

            var oldRadius = geo.RediusMeters;
            geo.RediusMeters = (decimal)newRadius;
            await _repo.UpdateAsync(geo);

            var audit = new GeoRadiusAudit
            {
                GeolocationId = id,
                OldRadius = oldRadius,
                NewRadius = (decimal)newRadius
            };
            await _repo.AddAuditAsync(audit);

            return geo;
        }
    }
}
