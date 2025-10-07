using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Services;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _service;

        public InstructorController(IInstructorService service)
        {
            _service = service;
        }

        // ============================================================
        // BASIC CRUD-LIKE OPERATIONS
        // ============================================================

        [HttpGet("GetInstructorById")]
        public async Task<IActionResult> GetById(int id)
        {
            var instructor = await _service.GetAsync(id);
            if (instructor == null) return NotFound($"Instructor {id} not found.");
            return Ok(instructor);
        }

        [HttpGet("GetAllInstructors")]
        public IActionResult GetAll([FromQuery] PagingFilter filter)
        {
            var instructors = _service.GetAll(filter);
            return Ok(instructors);
        }

        [HttpPost("CreateInstructor")]
        public async Task<IActionResult> Create([FromBody] InstructorCreateDto dto)
        {
            var created = await _service.CreateFromUserAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.InstructorId }, created);
        }

        [HttpPut("UpdateInstructor/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] InstructorUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpDelete("DeleteInstructor/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        // ============================================================
        // FILTERING ENDPOINTS
        // ============================================================

        /// <summary>
        /// Filter instructors by years of experience range.
        /// Example: /Instructor/FilterByExperienceYears?min=3&max=10
        /// </summary>
        [HttpGet("FilterByExperienceYears")]
        public IActionResult FilterByExperienceYears([FromQuery] int? min, [FromQuery] int? max)
        {
            var instructors = _service.GetAll(new PagingFilter { Page = 1, PageSize = 9999 });

            var filtered = instructors.Where(i =>
                (!min.HasValue || i.Years_of_Experience >= min) &&
                (!max.HasValue || i.Years_of_Experience <= max));

            return Ok(filtered);
        }

        /// <summary>
        /// Filter instructors by teaching style.
        /// Example: /Instructor/FilterByTeachingStyle?style=ProjectBased
        /// </summary>
        [HttpGet("FilterByTeachingStyle")]
        public IActionResult FilterByTeachingStyle([FromQuery] TeachingStyle style)
        {
            var instructors = _service.GetAll(new PagingFilter { Page = 1, PageSize = 9999 });
            var filtered = instructors.Where(i => i.Teaching_Style == style);
            return Ok(filtered);
        }

        /// <summary>
        /// Filter instructors by specialization (expertise).
        /// Example: /Instructor/FilterBySpecialization?spec=WebDevelopment
        /// </summary>
        [HttpGet("FilterBySpecialization")]
        public IActionResult FilterBySpecialization([FromQuery] Specializations spec)
        {
            var instructors = _service.GetAll(new PagingFilter { Page = 1, PageSize = 9999 });
            var filtered = instructors.Where(i =>
                i is { } && i.GetType().GetProperty("Specialization") != null &&
                ((Specializations)i.GetType().GetProperty("Specialization")!.GetValue(i)!) == spec);

            return Ok(filtered);
        }

        /// <summary>
        /// Combined filter for multiple criteria.
        /// Example: /Instructor/FilterCombined?style=HandsOn&spec=Java&min=2&max=10
        /// </summary>
        [HttpGet("FilterCombined")]
        public IActionResult FilterCombined(
            [FromQuery] TeachingStyle? style,
            [FromQuery] Specializations? spec,
            [FromQuery] int? min,
            [FromQuery] int? max)
        {
            var instructors = _service.GetAll(new PagingFilter { Page = 1, PageSize = 9999 });

            var filtered = instructors.Where(i =>
                (!style.HasValue || i.Teaching_Style == style) &&
                (!spec.HasValue || i.GetType().GetProperty("Specialization")?.GetValue(i)?.Equals(spec) == true) &&
                (!min.HasValue || i.Years_of_Experience >= min) &&
                (!max.HasValue || i.Years_of_Experience <= max));

            return Ok(filtered);
        }
    }
}
