using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/Geolocation")]
    public class GeolocationController: ControllerBase
    {
        private readonly IGeolocationRepository _repo;

        public GeolocationController(IGeolocationRepository repo)
        {
            _repo = repo;
        }
        // Retrieve all geolocations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var geolocations = await _repo.GetAllAsync();
            return Ok(geolocations);
        }
        // Retrieve geolocation by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var geolocation = await _repo.GetByIdAsync(id);
            if (geolocation == null)
                return NotFound();

            return Ok(geolocation);
        }

        // Create a new geolocation
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Geolocation geolocation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _repo.AddAsync(geolocation);
            return CreatedAtAction(nameof(Get), new { id = created.GeolocationId }, created);
        }
        // Update an existing geolocation
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Geolocation geolocation)
        {
            if (id != geolocation.GeolocationId)
                return BadRequest("ID mismatch");

            var updated = await _repo.UpdateAsync(geolocation);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // Delete a geolocation by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPatch("{id}/radius")]
        public async Task<IActionResult> UpdateRadius(int id, [FromBody] double newRadius)
        {
            var geolocation = await _repo.GetByIdAsync(id);
            if (geolocation == null)
                return NotFound();

            var oldRadius = geolocation.RediusMeters;
            geolocation.RediusMeters = (decimal)newRadius;
            await _repo.UpdateAsync(geolocation);

            var audit = new GeoRadiusAudit
            {
                GeolocationId = id,
                OldRadius = oldRadius,
                NewRadius = (decimal)newRadius,
            };

           await _repo.AddAuditAsync(audit);
            return Ok(geolocation);


        }
    }
}
