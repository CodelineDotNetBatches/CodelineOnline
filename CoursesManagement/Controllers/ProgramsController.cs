using CoursesManagement.DTOs;
using CoursesManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoursesManagement.Controllers
{

    // Manages CRUD operations for programs.
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramsService _service;

        public ProgramsController(IProgramsService service)
        {
            _service = service;
        }

        // GET: api/programs

        [HttpGet] 
        [ProducesResponseType(typeof(IEnumerable<ProgramDetailsDto>), 200)]  // Represents a successful response with a list of ProgramDetailsDto
        [SwaggerResponse(200, "List of all programs retrieved successfully.", typeof(IEnumerable<ProgramDetailsDto>))] // Swagger documentation for a successful response
        public async Task<IActionResult> GetAllPrograms()
        {
            var result = await _service.GetAllProgramsAsync();
            return Ok(result);
        }


        // GET: api/programs/{id}
        
        [HttpGet("{id:guid}")] // Route to get a program by its GUID
        [ProducesResponseType(typeof(ProgramDetailsDto), 200)] // Represents a successful response with a ProgramDetailsDto
        [ProducesResponseType(404)] // Represents a not found response
        [SwaggerResponse(200, "Program details retrieved successfully.", typeof(ProgramDetailsDto))] // Swagger documentation for a successful response
        [SwaggerResponse(404, "Program not found.")] // Swagger documentation for a not found response
        public async Task<IActionResult> GetProgramById(Guid id)
        {
            var result = await _service.GetProgramByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        
        // POST: api/programs

        [HttpPost]
        [ProducesResponseType(typeof(ProgramDetailsDto), 201)] // Represents a successful creation response with a ProgramDetailsDto
        [ProducesResponseType(400)] // Represents a bad request response
        [SwaggerResponse(201, "Program created successfully.", typeof(ProgramDetailsDto))] // Swagger documentation for a successful creation response
        [SwaggerResponse(400, "Invalid request data or duplicate program.")] // Swagger documentation for a bad request response
        public async Task<IActionResult> CreateProgram([FromBody] ProgramCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateProgramAsync(dto);
            return CreatedAtAction(nameof(GetProgramById), new { id = result.ProgramId }, result);
        }

        
        // PUT: api/programs/{id}

        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)] // Represents a successful update response with no content
        [ProducesResponseType(404)] // Represents a not found response
        [SwaggerResponse(204, "Program updated successfully.")] // Swagger documentation for a successful update response
        [SwaggerResponse(404, "Program not found.")] // Swagger documentation for a not found response
        public async Task<IActionResult> UpdateProgram(Guid id, [FromBody] ProgramUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateProgramAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/programs/{id}

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)] // Represents a successful deletion response with no content
        [ProducesResponseType(404)] // Represents a not found response
        [SwaggerResponse(204, "Program deleted successfully.")] // Swagger documentation for a successful deletion response
        [SwaggerResponse(404, "Program not found.")] // Swagger documentation for a not found response
        public async Task<IActionResult> DeleteProgram(Guid id)
        {
            await _service.DeleteProgramAsync(id);
            return NoContent();
        }
    }


}
