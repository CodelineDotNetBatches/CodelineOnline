using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Services;
using static ReportsManagements.DTOs.GeolocationDTO;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/v1/geolocation")]
    public class GeolocationController : ControllerBase
    {
        private readonly GeolocationService _service;

        public GeolocationController(GeolocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var geos = await _service.GetAllAsync();
            var result = geos.Select(g => new GeolocationResponseDto
            {
                GeolocationId = g.GeolocationId,
                Latitude = g.Latitude,
                Longitude = g.Longitude,
                RediusMeters = g.RediusMeters,
                IsActive = g.IsActive,
                BranchId = g.BranchId
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var geo = await _service.GetByIdAsync(id);
            if (geo == null) return NotFound();

            return Ok(new GeolocationResponseDto
            {
                GeolocationId = geo.GeolocationId,
                Latitude = geo.Latitude,
                Longitude = geo.Longitude,
                RediusMeters = geo.RediusMeters,
                IsActive = geo.IsActive,
                BranchId = geo.BranchId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeolocationCreateDto dto)
        {
            var geo = new Geolocation
            {
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                RediusMeters = dto.RediusMeters,
                BranchId = dto.BranchId,
                IsActive = true
            };

            var created = await _service.CreateAsync(geo);

            return CreatedAtAction(nameof(Get), new { id = created.GeolocationId }, new GeolocationResponseDto
            {
                GeolocationId = created.GeolocationId,
                Latitude = created.Latitude,
                Longitude = created.Longitude,
                RediusMeters = created.RediusMeters,
                IsActive = created.IsActive,
                BranchId = created.BranchId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Geolocation geo)
        {
            if (id != geo.GeolocationId) return BadRequest("ID mismatch");

            var updated = await _service.UpdateAsync(geo);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/radius")]
        public async Task<IActionResult> UpdateRadius(int id, [FromBody] GeoRadiusUpdateDto dto)
        {
            var updated = await _service.UpdateRadiusAsync(id, dto.NewRadius);
            if (updated == null) return NotFound();

            return Ok(new GeolocationResponseDto
            {
                GeolocationId = updated.GeolocationId,
                Latitude = updated.Latitude,
                Longitude = updated.Longitude,
                RediusMeters = updated.RediusMeters,
                IsActive = updated.IsActive,
                BranchId = updated.BranchId
            });
        }
    }
}
