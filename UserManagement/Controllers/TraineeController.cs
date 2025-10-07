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
    public class TraineeController : ControllerBase
    {
        private readonly ITraineeService _service;

        public TraineeController(ITraineeService service)
        {
            _service = service;
        }

        // ============================================================
        // BASIC CRUD
        // ============================================================

        /// <summary>
        /// Get all trainees.
        /// </summary>
        [HttpGet("GetAllTrainees")]
        public async Task<IActionResult> GetAll()
        {
            var trainees = await _service.GetAllTraineesAsync();
            return Ok(trainees);
        }

        /// <summary>
        /// Get a trainee by ID.
        /// </summary>
        [HttpGet("GetTraineeById")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainee = await _service.GetTraineeByIdAsync(id);
            if (trainee == null) return NotFound();
            return Ok(trainee);
        }

        /// <summary>
        /// Create a new trainee.
        /// </summary>
        [HttpPost("CreateNewTrainee")]
        public async Task<IActionResult> Create([FromBody] TraineeDTO dto)
        {
            var created = await _service.CreateTraineeAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TraineeId }, created);
        }

        /// <summary>
        /// Update an existing trainee.
        /// </summary>
        [HttpPut("UpdateTraineeById")]
        public async Task<IActionResult> Update(int id, [FromBody] TraineeDTO dto)
        {
            if (id != dto.TraineeId) return BadRequest();

            var updated = await _service.UpdateTraineeAsync(dto);
            return Ok(updated);
        }

        /// <summary>
        /// Delete a trainee by ID.
        /// </summary>
        [HttpDelete("DeleteTraineeById")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteTraineeAsync(id);
            return NoContent();
        }

        // ============================================================
        // FILTERING ENDPOINTS
        // ============================================================

        /// <summary>
        /// Filter trainees by minimum and maximum years of experience.
        /// </summary>
        [HttpGet("FilterByExperienceYears")]
        public async Task<IActionResult> FilterByExperienceYears([FromQuery] int? min, [FromQuery] int? max)
        {
            var trainees = await _service.GetAllTraineesAsync();
            var filtered = trainees.Where(t =>
                (!min.HasValue || t.Years_of_Experience >= min) &&
                (!max.HasValue || t.Years_of_Experience <= max));
            return Ok(filtered);
        }

        /// <summary>
        /// Filter trainees by experience level (Junior, Mid, Senior, Lead).
        /// </summary>
        [HttpGet("FilterByExperienceLevel")]
        public async Task<IActionResult> FilterByExperienceLevel([FromQuery] ExperienceLevel level)
        {
            var trainees = await _service.GetAllTraineesAsync();
            var filtered = trainees.Where(t => t.ExperienceLevel == level.ToString());
            return Ok(filtered);
        }

        /// <summary>
        /// Filter trainees by learning style (e.g., ProjectBased, SelfPaced, etc.).
        /// </summary>
        [HttpGet("FilterByLearningStyle")]
        public async Task<IActionResult> FilterByLearningStyle([FromQuery] LearningStyle style)
        {
            var trainees = await _service.GetAllTraineesAsync();
            var filtered = trainees.Where(t => t.Learning_Style == style);
            return Ok(filtered);
        }

        /// <summary>
        /// Filter trainees by study focus (e.g., CSharp, WebDevelopment, etc.).
        /// </summary>
        [HttpGet("FilterByStudyFocus")]
        public async Task<IActionResult> FilterByStudyFocus([FromQuery] StudyFocus focus)
        {
            var trainees = await _service.GetAllTraineesAsync();
            var filtered = trainees.Where(t => t.Study_Focus == focus);
            return Ok(filtered);
        }

        /// <summary>
        /// Combined filter by multiple criteria.
        /// Example:
        /// /Trainee/FilterCombined?level=Mid&style=GroupStudy&focus=WebDevelopment&min=1&max=5
        /// </summary>
     
    
    }
}
