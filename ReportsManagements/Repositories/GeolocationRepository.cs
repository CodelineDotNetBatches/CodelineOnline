using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;

namespace ReportsManagements.Repositories
 
{
    public class GeolocationRepository : IGeolocationRepository
    {
        private readonly ReportsDbContext _context;

        public GeolocationRepository(ReportsDbContext context)
        {
            _context = context;
        }

        //Get all geolocations
        public async Task<IEnumerable<Geolocation>> GetAllAsync()
        {
            return await _context.Geolocations.Where(g => g.IsActive).ToListAsync();
        }
            
        //Get geoloction by id
        public async Task<Geolocation?> GetByIdAsync(int id) =>
            await _context.Geolocations.FindAsync(id);

        //add new geolocation
        public async Task<Geolocation> AddAsync(Geolocation geolocation)
        {
            _context.Geolocations.Add(geolocation);
            await _context.SaveChangesAsync();
            return geolocation;
        }
        //Update existing geolocation 
        public async Task<Geolocation> UpdateAsync(Geolocation geolocation)
        {
            _context.Geolocations.Update(geolocation);
            await _context.SaveChangesAsync();
            return geolocation;
        }
        // Delete geolocation by Id

        public async Task<bool> DeleteAsync(int id)
        {
            var geolocation = await _context.Geolocations.FindAsync(id);
            if (geolocation==null) 
            return false;

            geolocation.IsActive = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAuditAsync(GeoRadiusAudit audit)
        {
            _context.GeoRadiusAudits.Add(audit);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetGeolocationsCountAsync() => await _context.Geolocations.CountAsync();
        public async Task<int> GetActiveGeolocationsCountAsync() => await _context.Geolocations.CountAsync(g => g.IsActive);

    }
}
