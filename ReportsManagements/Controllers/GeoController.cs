using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.Services;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/v1/geo")]
    public class GeoController: ControllerBase
    {
        private readonly IGeoValidationService _geoService;
        private readonly IGeolocationRepository _geoRepo;

        public GeoController(IGeoValidationService geoService, IGeolocationRepository geoRepo)
        {
            _geoService = geoService;
            _geoRepo = geoRepo;
        }

        // Endpoint to check if a point is within a circle
        //POST /api/v1/geo/validate
        [HttpPost("validate")]
        public async Task<IActionResult> Validate([FromBody] Geolocation request)
        {
            var geo = await _geoRepo.GetByIdAsync(request.GeolocationId);
            if (geo == null)
                return NotFound("Geolocation not found.");

            if(!double.TryParse(geo.Latitude, out double centerLat) || !double.TryParse(geo.Longitude, out double centerLon))
                return BadRequest("Invalid geolocation coordinates.");

            var isInside = _geoService.IsPointInsideCircle(
                double.Parse(request.Latitude),
                double.Parse(request.Longitude),
                centerLat,
                centerLon,
                (double)geo.RediusMeters,
                out double distanceMeters);

                return Ok(new { isInside }
            );
        }
    }
}
