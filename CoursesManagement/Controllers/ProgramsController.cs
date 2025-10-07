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

        [HttpGet("GetAllPrograms")]
        public async Task<IActionResult> GetAllPrograms()
        {
            try
            {
                var result = await _service.GetAllProgramsAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(200, $"List of all programs retrieved successfully. {ex.Message}");
            }
        }


        // GET: api/programs/{id}

        [HttpGet("GetProgramById{id:guid}")] // Route to get a program by its GUID
        public async Task<IActionResult> GetProgramById(Guid id)
        {
            try
            {
                var result = await _service.GetProgramByIdAsync(id);

                if (result == null)
                    return NotFound("Program not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(200, $"Program details retrieved successfully. {ex.Message}");
            }

        }


        // GET: api/programs/name/{programName}
        [HttpGet("GetProgramByName{programName}")]
        public async Task<IActionResult> GetProgramByName(string programName)
        {
            try
            {
                var result = await _service.GetProgramByNameAsync(programName);
                if (result == null)
                    return NotFound("Program not found.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve program by name. {ex.Message}");
            }
        }



        // POST: api/programs

        [HttpPost("CreateProgram")]
        [ProducesResponseType(typeof(ProgramDetailsDto), 201)] // Represents a successful creation response with a ProgramDetailsDto
        [ProducesResponseType(400)] // Represents a bad request response
        //[SwaggerResponse(201, "Program created successfully.", typeof(ProgramDetailsDto))] // Swagger documentation for a successful creation response
        //[SwaggerResponse(400, "Invalid request data or duplicate program.")] // Swagger documentation for a bad request response
        public async Task<IActionResult> CreateProgram([FromBody] ProgramCreateDto dto)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _service.CreateProgramAsync(dto);
                return CreatedAtAction(nameof(GetProgramById), new { id = result.ProgramId }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(200, $"Program created successfully. {ex.Message}");
            }
        }


        // PUT: api/programs/{id}

        [HttpPut("UpdateProgramByProgramId{id:guid}")]
        [ProducesResponseType(204)] // Represents a successful update response with no content
        [ProducesResponseType(404)] // Represents a not found response
        //[SwaggerResponse(204, "Program updated successfully.")] // Swagger documentation for a successful update response
        //[SwaggerResponse(404, "Program not found.")] // Swagger documentation for a not found response
        public async Task<IActionResult> UpdateProgram(Guid id, [FromBody] ProgramUpdateDto dto)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _service.UpdateProgramAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(200, $"Program updated successfully. {ex.Message}");
            }
        }

        // DELETE: api/programs/{id}

        [HttpDelete("DeleteProgramByProgramId{id:guid}")]
        [ProducesResponseType(204)] // Represents a successful deletion response with no content
        [ProducesResponseType(404)] // Represents a not found response
        //[SwaggerResponse(204, "Program deleted successfully.")] // Swagger documentation for a successful deletion response
        //[SwaggerResponse(404, "Program not found.")] // Swagger documentation for a not found response
        public async Task<IActionResult> DeleteProgram(Guid id)
        {
            try
            {
                await _service.DeleteProgramAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(200, $"Program deleted successfully. {ex.Message}");
            }


        }


        //// GET: api/programs/{id}/courses

        [HttpGet("GetProgramWithCoursesByProgramId{id:guid}")]
        [ProducesResponseType(204)] // Represents a successful deletion response with no content
        [ProducesResponseType(404)] // Represents a not found response
        public async Task<IActionResult> GetProgramWithCourses(Guid id)
        {
            try
            {
                var result = await _service.GetProgramByIdAsync(id);
                if (result == null)
                    return NotFound("Program not found.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve program with courses. {ex.Message}");
            }
        }

        // Get all Courses in a Program
        //[HttpGet("GetAllCoursesInProgramByProgramId {id:guid}")]
        //[ProducesResponseType(204)] // Represents a successful deletion response with no content
        //[ProducesResponseType(404)] // Represents a not found response
        //public async Task<IActionResult> GetAllCoursesInProgramByProgramId(Guid id)
        //{
        //    try
        //    {
        //        var result = await _service.GetProgramWithCoursesAsync(id);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Failed to retrieve courses for program. {ex.Message}");
        //    }


        //}


        // GET: api/programs/{id}/categories
        [HttpGet("GetProgramWithCategoriesByProgramId{id:guid}")]
        public async Task<IActionResult> GetProgramWithCategories(Guid id)
        {
            try
            {
                var result = await _service.GetProgramWithCategoriesAsync(id);
                if (result == null)
                    return NotFound("Program not found.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve program with categories. {ex.Message}");
            }
        }

        // GET: api/programs/{id}/Enrollments
        [HttpGet("GetAllEnrollmentsInProgramByProgramId{id:guid}")]
        public async Task<IActionResult> GetAllEnrollmentsInProgramByProgramId(Guid id)
        {
            try
            {
                var result = await _service.GetProgramWithEnrollmentsAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve enrollments for program. {ex.Message}");
            }
        }


    }

}
