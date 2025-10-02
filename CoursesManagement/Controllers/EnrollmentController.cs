using CoursesManagement.DTOs;
using CoursesManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CoursesManagement.Controllers
{
    /// <summary>
    /// API controller for managing course enrollments.
    /// Provides endpoints for CRUD operations on enrollments.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnrollmentController"/>.
        /// </summary>
        /// <param name="service">The enrollment service (business logic layer).</param>
        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all enrollments in summary view.
        /// </summary>
        [HttpGet]
        [SwaggerResponse(200, "List of enrollments", typeof(IEnumerable<EnrollmentListDto>))]
        public async Task<ActionResult<IEnumerable<EnrollmentListDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets an enrollment by ID (detailed view).
        /// </summary>
        /// <param name="id">The enrollment ID.</param>
        [HttpGet("{id:guid}")]
        [SwaggerResponse(200, "Enrollment details", typeof(EnrollmentDetailDto))]
        [SwaggerResponse(404, "Enrollment not found")]
        public async Task<ActionResult<EnrollmentDetailDto>> GetById(Guid id)
        {
            var enrollment = await _service.GetByIdAsync(id);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        /// <summary>
        /// Gets all enrollments for a specific user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        [HttpGet("user/{userId:guid}")]
        [SwaggerResponse(200, "Enrollments for a user", typeof(IEnumerable<EnrollmentListDto>))]
        public async Task<ActionResult<IEnumerable<EnrollmentListDto>>> GetByUser(Guid userId)
        {
            var result = await _service.GetByUserIdAsync(userId);
            return Ok(result);
        }

        /// <summary>
        /// Gets all enrollments for a specific course.
        /// </summary>
        /// <param name="courseId">The course ID.</param>
        [HttpGet("course/{courseId:guid}")]
        [SwaggerResponse(200, "Enrollments for a course", typeof(IEnumerable<EnrollmentListDto>))]
        public async Task<ActionResult<IEnumerable<EnrollmentListDto>>> GetByCourse(Guid courseId)
        {
            var result = await _service.GetByCourseIdAsync(courseId);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new enrollment.
        /// </summary>
        /// <param name="dto">The create enrollment DTO.</param>
        [HttpPost]
        [SwaggerResponse(201, "Enrollment created successfully", typeof(EnrollmentDetailDto))]
        [SwaggerResponse(400, "Invalid request or duplicate enrollment")]
        public async Task<ActionResult<EnrollmentDetailDto>> Create([FromBody] CreateEnrollmentDto dto)
        {
            try
            {
                var enrollment = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = enrollment.EnrollmentId }, enrollment);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Updates an enrollment (status, grade, or status change reason).
        /// </summary>
        /// <param name="id">The enrollment ID.</param>
        /// <param name="dto">The update enrollment DTO.</param>
        [HttpPut("{id:guid}")]
        [SwaggerResponse(200, "Enrollment updated successfully", typeof(EnrollmentDetailDto))]
        [SwaggerResponse(404, "Enrollment not found")]
        public async Task<ActionResult<EnrollmentDetailDto>> Update(Guid id, [FromBody] UpdateEnrollmentDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        /// <summary>
        /// Deletes an enrollment.
        /// </summary>
        /// <param name="id">The enrollment ID.</param>
        [HttpDelete("{id:guid}")]
        [SwaggerResponse(204, "Enrollment deleted successfully")]
        [SwaggerResponse(404, "Enrollment not found")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
