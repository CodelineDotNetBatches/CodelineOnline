using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IAvailabilityService _svc;
        public AvailabilitiesController(IAvailabilityService svc) => _svc = svc;

        // -------------------------------
        // POST: Create new availability
        // -------------------------------
        [HttpPost("AddNewAvailability")]
        public async Task<ActionResult<AvailabilityReadDto>> Create(
            [FromRoute] int instructorId,
            [FromBody] AvailabilityCreateDto dto,
            CancellationToken ct)
        {
            dto.InstructorId = instructorId;
            var created = await _svc.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetByInstructor), new { instructorId }, created);
        }

        // -------------------------------
        // GET: All availabilities by instructor
        // -------------------------------
        [HttpGet("GetAllAvailabilities")]
        public async Task<ActionResult<IEnumerable<AvailabilityReadDto>>> GetByInstructor(
            [FromRoute] int instructorId,
            CancellationToken ct)
        {
            var list = await _svc.GetByInstructorAsync(instructorId, ct);
            return Ok(list);
        }

        // -------------------------------
        // PUT: Update an availability
        // -------------------------------
        [HttpPut("UpdateAavailabilityById")]
        public async Task<ActionResult<AvailabilityReadDto>> Update(
            [FromRoute] int instructorId,
            [FromRoute] int availabilityId,
            [FromBody] AvailabilityUpdateDto dto,
            CancellationToken ct)
        {
            var updated = await _svc.UpdateAsync(instructorId, availabilityId, dto, ct);
            return Ok(updated);
        }

        // -------------------------------
        // DELETE: Remove an availability
        // -------------------------------
        [HttpDelete("DeleteAvailabilityById")]
        public async Task<IActionResult> Delete(
            [FromRoute] int instructorId,
            [FromRoute] int availabilityId,
            CancellationToken ct)
        {
            await _svc.RemoveAsync(instructorId, availabilityId, ct);
            return NoContent();
        }

        // -------------------------------
        // GET: Instructor availability calendar
        // -------------------------------
        [HttpGet("GetAvailabilityByCalendar")]
        public async Task<ActionResult<IEnumerable<AvailabilityReadDto>>> GetCalendar(
            [FromRoute] int instructorId,
            CancellationToken ct)
        {
            var list = await _svc.GenerateCalendarAsync(instructorId, ct);
            return Ok(list);
        }
    }
}
