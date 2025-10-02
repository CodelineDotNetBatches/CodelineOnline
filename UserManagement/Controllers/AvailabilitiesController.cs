
using AutoMapper;
using Microsoft.AspNetCore.Mvc; 
using UserManagement.DTOs;
using UserManagement.Services;
namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailabilitiesController : Controller
    {
        private readonly IAvailabilityService _svc;
        public AvailabilitiesController(IAvailabilityService svc) => _svc = svc;

        [HttpPost]
        public async Task<ActionResult<AvailabilityReadDto>> Create(
            [FromRoute] int instructorId, [FromBody] AvailabilityCreateDto dto, CancellationToken ct)
        {
            // make sure route and body InstructorId align
            dto.InstructorId = instructorId;
            var created = await _svc.AddAsync(dto, ct);
            return CreatedAtAction(nameof(GetByInstructor), new { instructorId }, created);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AvailabilityReadDto>>> GetByInstructor([FromRoute] int instructorId, CancellationToken ct)
        {
            var list = await _svc.GetByInstructorAsync(instructorId, ct);
            return Ok(list);
        }

        [HttpPut("{availabilityId:int}")]
        public async Task<ActionResult<AvailabilityReadDto>> Update(
            [FromRoute] int instructorId, [FromRoute] int availabilityId, [FromBody] AvailabilityUpdateDto dto, CancellationToken ct)
        {
            var updated = await _svc.UpdateAsync(instructorId, availabilityId, dto, ct);
            return Ok(updated);
        }

        [HttpDelete("{availabilityId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int instructorId, [FromRoute] int availabilityId, CancellationToken ct)
        {
            await _svc.RemoveAsync(instructorId, availabilityId, ct);
            return NoContent();
        }

        // calendar helper
        [HttpGet("calendar")]
        public async Task<ActionResult<IEnumerable<AvailabilityReadDto>>> GetCalendar([FromRoute] int instructorId, CancellationToken ct)
        {
            var list = await _svc.GenerateCalendarAsync(instructorId, ct);
            return Ok(list);
        }
    }
}
