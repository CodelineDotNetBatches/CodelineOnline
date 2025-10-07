using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using static ReportsManagements.DTOs.GeolocationDTO;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/geolocations")]
    public class GeolocationController : ControllerBase
    {
        private readonly IGeolocationRepository _repo;

        public GeolocationController(IGeolocationRepository repo)
        {
            _repo = repo;
        }

        // ===== Get All =====
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var geos = await _repo.GetAllAsync();
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

        // ===== Get by Id =====
        [HttpGet("view/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var geo = await _repo.GetByIdAsync(id);
            if (geo == null)
                return NotFound();

            var dto = new GeolocationResponseDto
            {
                GeolocationId = geo.GeolocationId,
                Latitude = geo.Latitude,
                Longitude = geo.Longitude,
                RediusMeters = geo.RediusMeters,
                IsActive = geo.IsActive,
                BranchId = geo.BranchId
            };

            return Ok(dto);
        }

        // ===== Create =====
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] GeolocationCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var geo = new Geolocation
            {
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                RediusMeters = dto.RediusMeters,
                BranchId = dto.BranchId,
                IsActive = true
            };

            var created = await _repo.AddAsync(geo);

            var response = new GeolocationResponseDto
            {
                GeolocationId = created.GeolocationId,
                Latitude = created.Latitude,
                Longitude = created.Longitude,
                RediusMeters = created.RediusMeters,
                IsActive = created.IsActive,
                BranchId = created.BranchId
            };

            return CreatedAtAction(nameof(GetById), new { id = created.GeolocationId }, response);
        }

        // ===== Update =====
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Geolocation geo)
        {
            if (id != geo.GeolocationId)
                return BadRequest("ID mismatch");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.Latitude = geo.Latitude;
            existing.Longitude = geo.Longitude;
            existing.RediusMeters = geo.RediusMeters;
            existing.IsActive = geo.IsActive;
            existing.BranchId = geo.BranchId;

            var updated = await _repo.UpdateAsync(existing);
            return Ok(updated);
        }

        // ===== Delete =====
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var geo = await _repo.GetByIdAsync(id);
            if (geo == null)
                return NotFound();

            await _repo.DeleteAsync(id);
            return NoContent();
        }

        // ===== Update Radius Only =====
        [HttpPatch("update-radius/{id}")]
        public async Task<IActionResult> UpdateRadius(int id, [FromBody] GeoRadiusUpdateDto dto)
        {
            var geo = await _repo.GetByIdAsync(id);
            if (geo == null)
                return NotFound();

            geo.RediusMeters = (decimal)dto.NewRadius;
            var updated = await _repo.UpdateAsync(geo);

            var response = new GeolocationResponseDto
            {
                GeolocationId = updated.GeolocationId,
                Latitude = updated.Latitude,
                Longitude = updated.Longitude,
                RediusMeters = updated.RediusMeters,
                IsActive = updated.IsActive,
                BranchId = updated.BranchId
            };

            return Ok(response);
        }
    }
}
