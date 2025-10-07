using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IAvailabilityService _svc;
        public AvailabilitiesController(IAvailabilityService svc) => _svc = svc;

        // ============================================================
        // CRUD-LIKE ENDPOINTS
        // ============================================================

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

        [HttpGet("GetAllAvailabilities")]
        public async Task<ActionResult<IEnumerable<AvailabilityReadDto>>> GetByInstructor(
            [FromRoute] int instructorId,
            CancellationToken ct)
        {
            var list = await _svc.GetByInstructorAsync(instructorId, ct);
            return Ok(list);
        }

        [HttpPut("UpdateAvailabilityById")]
        public async Task<ActionResult<AvailabilityReadDto>> Update(
            [FromRoute] int instructorId,
            [FromRoute] int availabilityId,
            [FromBody] AvailabilityUpdateDto dto,
            CancellationToken ct)
        {
            var updated = await _svc.UpdateAsync(instructorId, availabilityId, dto, ct);
            return Ok(updated);
        }

        [HttpDelete("DeleteAvailabilityById")]
        public async Task<IActionResult> Delete(
            [FromRoute] int instructorId,
            [FromRoute] int availabilityId,
            CancellationToken ct)
        {
            await _svc.RemoveAsync(instructorId, availabilityId, ct);
            return NoContent();
        }

        [HttpGet("GetAvailabilityByCalendar")]
        public async Task<ActionResult<IEnumerable<AvailabilityReadDto>>> GetCalendar(
            [FromRoute] int instructorId,
            CancellationToken ct)
        {
            var list = await _svc.GenerateCalendarAsync(instructorId, ct);
            return Ok(list);
        }

        // ============================================================
        // FILTERING ENDPOINTS
        // ============================================================

        /// <summary>
        /// Filter availabilities by status (e.g. Available, Unavailable).
        /// Example: /Availabilities/FilterByStatus?instructorId=5&status=Available
        /// </summary>
        [HttpGet("FilterByStatus")]
        public async Task<IActionResult> FilterByStatus(
            [FromQuery] int instructorId,
            [FromQuery] AvailabilityStatus status,
            CancellationToken ct)
        {
            var all = await _svc.GetByInstructorAsync(instructorId, ct);
            var filtered = all.Where(a => a.Avail_Status == status).ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter availabilities by day of week (e.g. Monday, Tuesday).
        /// Example: /Availabilities/FilterByDay?instructorId=5&day=Monday
        /// </summary>
        [HttpGet("FilterByDay")]
        public async Task<IActionResult> FilterByDay(
            [FromQuery] int instructorId,
            [FromQuery] DaysOfWeek day,
            CancellationToken ct)
        {
            var all = await _svc.GetByInstructorAsync(instructorId, ct);
            var filtered = all.Where(a => a.Day_Of_Week == day).ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter availabilities by specific time (e.g. 09:00, 14:30).
        /// Example: /Availabilities/FilterByTime?instructorId=5&time=09:00
        /// </summary>
        [HttpGet("FilterByTime")]
        public async Task<IActionResult> FilterByTime(
            [FromQuery] int instructorId,
            [FromQuery] TimeOnly time,
            CancellationToken ct)
        {
            var all = await _svc.GetByInstructorAsync(instructorId, ct);
            var filtered = all.Where(a => a.Time == time).ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Combined filter (status, day, and time).
        /// Example: /Availabilities/FilterCombined?instructorId=5&status=Available&day=Monday&time=09:00
        /// </summary>
        [HttpGet("FilterCombined")]
        public async Task<IActionResult> FilterCombined(
            [FromQuery] int instructorId,
            [FromQuery] AvailabilityStatus? status,
            [FromQuery] DaysOfWeek? day,
            [FromQuery] TimeOnly? time,
            CancellationToken ct)
        {
            var all = await _svc.GetByInstructorAsync(instructorId, ct);

            var filtered = all.Where(a =>
                (!status.HasValue || a.Avail_Status == status) &&
                (!day.HasValue || a.Day_Of_Week == day) &&
                (!time.HasValue || a.Time == time))
                .ToList();

            return Ok(filtered);
        }
    }
}
