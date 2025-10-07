using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.Services;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("geo")]
    public class GeoController: ControllerBase
    {
        private readonly IGeoValidationService _geoService;
        private readonly IGeolocationRepository _geoRepo;
        private readonly IBranchRepository _branchRepo;

        public GeoController(IGeoValidationService geoService, IGeolocationRepository geoRepo,IBranchRepository branchRepository)
        {
            _geoService = geoService;
            _geoRepo = geoRepo;
            _branchRepo = branchRepository;
        }

        [HttpPost("validate-point")]
        public async Task<IActionResult> Validate([FromBody] GeoValidateRequest request)
        {
            var geo = await _geoRepo.GetByIdAsync(request.GeolocationId);
            if (geo == null)
                return NotFound("Geolocation not found.");

            if (!double.TryParse(request.Latitude, out double lat) || !double.TryParse(request.Longitude, out double lon))
                return BadRequest("Invalid coordinates.");

            if (!double.TryParse(geo.Latitude, out double centerLat) || !double.TryParse(geo.Longitude, out double centerLon))
                return BadRequest("Invalid geolocation coordinates.");

            var isInside = _geoService.IsPointInsideCircle(
                lat,
                lon,
                centerLat,
                centerLon,
                (double)geo.RediusMeters,
                out double distanceMeters);

            return Ok(new { isInside, distanceMeters });
        }

        [HttpGet("health-check")]
        public async Task<IActionResult> GetHealth()
        {
            var branchesCount = await _branchRepo.GetBranchCountAsync();
            var activeBranches = await _branchRepo.GetActiveBranchCountAsync();
            var geolocationsCount = await _geoRepo.GetGeolocationsCountAsync();
            var activeGeolocations = await _geoRepo.GetActiveGeolocationsCountAsync();

            var result = new
            {
                branchesCount,
                activeBranches,
                geolocationsCount,
                activeGeolocations
            };
            return Ok(result);
        }
        

    }
}
