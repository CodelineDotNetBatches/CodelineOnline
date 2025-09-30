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
        public async Task<IEnumerable<Geolocation>> GetAllAsync() =>
            await _context.Geolocations.ToListAsync();
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
            if (geolocation != null) return false;

            _context.Geolocations.Remove(geolocation);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
