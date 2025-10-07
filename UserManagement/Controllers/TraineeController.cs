using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Services;

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
    }
}
