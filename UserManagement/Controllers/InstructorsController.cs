using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorsController : Controller
    {
        private readonly IInstructorService _svc;
        public InstructorsController(IInstructorService svc) => _svc = svc;

        // Called after User module creates a user with role = Instructor
        [HttpPost("CreateInstructor")]
        public async Task<ActionResult<InstructorReadDto>> Create([FromBody] InstructorCreateDto dto, CancellationToken ct)
        {
            var created = await _svc.CreateFromUserAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.InstructorId }, created);
        }

        [HttpGet("GetInstructorById")]
        public async Task<ActionResult<InstructorReadDto>> GetById([FromRoute] int id, CancellationToken ct)
        {
            var result = await _svc.GetAsync(id, ct);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("GetAllInstructor")]
        public async Task<ActionResult<IEnumerable<InstructorReadDto>>> GetAll([FromQuery] PagingFilter filter, CancellationToken ct)
        {
            var list = await _svc.GetAllAsync(filter, ct);
            return Ok(list);
        }

        [HttpPut("UpdateInstructorById")]
        public async Task<ActionResult<InstructorReadDto>> Update([FromRoute] int id, [FromBody] InstructorUpdateDto dto, CancellationToken ct)
        {
            var updated = await _svc.UpdateAsync(id, dto, ct);
            return Ok(updated);
        }

        [HttpDelete("DeleteInstructorById")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken ct)
        {
            await _svc.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}

