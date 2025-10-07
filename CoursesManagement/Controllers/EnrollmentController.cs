using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CoursesManagement.Services;
using CoursesManagement.DTOs;

namespace CoursesManagement.Controllers
{
    /// <summary>
    /// Manages student enrollments in courses.
    /// Provides CRUD operations and course/user filters.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        /// <summary>
        /// Initializes a new instance of the EnrollmentController.
        /// </summary>
        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        // =========================================================
        // GET: api/enrollment
        // =========================================================
        /// <summary>
        /// Retrieves all enrollments.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentListDto>), 200)]
        [SwaggerResponse(200, "List of all enrollments retrieved successfully.", typeof(IEnumerable<EnrollmentListDto>))]
        public async Task<ActionResult<IEnumerable<EnrollmentListDto>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // =========================================================
        // GET: api/enrollment/{id}
        // =========================================================
        /// <summary>
        /// Retrieves enrollment details by ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(EnrollmentDetailDto), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(200, "Enrollment details retrieved successfully.", typeof(EnrollmentDetailDto))]
        [SwaggerResponse(404, "Enrollment not found.")]
        public async Task<ActionResult<EnrollmentDetailDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // =========================================================
        // GET: api/enrollment/course/{courseId}
        // =========================================================
        /// <summary>
        /// Retrieves enrollments for a specific course (by ID).
        /// </summary>
        [HttpGet("course/{courseId:guid}")]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentListDto>), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(200, "Enrollments for a specific course retrieved successfully.", typeof(IEnumerable<EnrollmentListDto>))]
        [SwaggerResponse(404, "Course not found or has no enrollments.")]
        public async Task<ActionResult<IEnumerable<EnrollmentListDto>>> GetByCourse(Guid courseId)
        {
            var result = await _service.GetByCourseIdAsync(courseId);
            if (result == null || !result.Any())
                return NotFound();
            return Ok(result);
        }

        // =========================================================
        //  GET: api/enrollment/course/name/{courseName}
        // =========================================================
        /// <summary>
        /// Retrieves enrollments for a specific course (by name).
        /// </summary>
        /// <remarks>s
        /// **Example Request:**
        /// ```
        /// GET /api/enrollment/course/name/CSharp%20Fundamentals
        /// ```
        /// 
        /// **Example Response:**
        /// ```json
        /// [
        ///   {
        ///     "enrollmentId": "b1e2e7d9-2d89-4e02-bc3a-7b26c6b4f6e9",
        ///     "userId": "63a2fc97-cb1e-43a4-9b9d-4c1b66ff48b0",
        ///     "userName": "Jane Doe",
        ///     "courseId": "91f258a7-3b29-4b2b-9c92-1ab4a65d8a44",
        ///     "courseTitle": "C# Fundamentals",
        ///     "programId": "cb8c8a0d-70d4-4e32-b8d6-33f2296f5f0d",
        ///     "programName": "Full Stack Developer Program",
        ///     "status": "Active",
        ///     "grade": 92.5,
        ///     "statusChangeReason": null,
        ///     "enrolledAt": "2025-09-28T14:45:00Z"
        ///   },
        ///   {
        ///     "enrollmentId": "c3f79b11-7ed0-4988-9c47-3b77aa3a9d22",
        ///     "userId": "3f45d2c7-95e2-4f6e-8c71-2afcbe2a12b9",
        ///     "userName": "Ahmed Al-Balushi",
        ///     "courseId": "91f258a7-3b29-4b2b-9c92-1ab4a65d8a44",
        ///     "courseTitle": "C# Fundamentals",
        ///     "programId": null,
        ///     "programName": null,
        ///     "status": "Completed",
        ///     "grade": 88.0,
        ///     "statusChangeReason": "Completed successfully",
        ///     "enrolledAt": "2025-09-12T09:30:00Z"
        ///   }
        /// ]
        /// ```
        /// </remarks>
        [HttpGet("course/name/{courseName}")]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentListDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [SwaggerResponse(200, "Enrollments for the given course name retrieved successfully.", typeof(IEnumerable<EnrollmentListDto>))]
        [SwaggerResponse(400, "Course name cannot be empty.")]
        [SwaggerResponse(404, "No enrollments found for the specified course name.")]
        public async Task<ActionResult<IEnumerable<EnrollmentListDto>>> GetByCourseName(string courseName)
        {
            if (string.IsNullOrWhiteSpace(courseName))
                return BadRequest("Course name cannot be empty.");

            var result = await _service.GetByCourseNameAsync(courseName);
            if (result == null || !result.Any())
                return NotFound($"No enrollments found for course '{courseName}'.");

            return Ok(result);
        }

        // =========================================================
        // POST: api/enrollment
        // =========================================================
        /// <summary>
        /// Creates a new enrollment.
        /// </summary>
        /// <remarks>
        /// **Example Request:**
        /// ```json
        /// {
        ///   "userId": "3f45d2c7-95e2-4f6e-8c71-2afcbe2a12b9",
        ///   "courseId": "91f258a7-3b29-4b2b-9c92-1ab4a65d8a44",
        ///   "programId": "cb8c8a0d-70d4-4e32-b8d6-33f2296f5f0d"
        /// }
        /// ```
        /// 
        /// **Example Response:**
        /// ```json
        /// {
        ///   "enrollmentId": "eec7c205-ecb0-4e18-8ad6-63c3c020d6df",
        ///   "userName": "Jane Doe",
        ///   "courseTitle": "C# Fundamentals",
        ///   "status": "Active",
        ///   "enrolledAt": "2025-09-28T14:45:00Z"
        /// }
        /// ```
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(typeof(EnrollmentDetailDto), 201)]
        [ProducesResponseType(400)]
        [SwaggerResponse(201, "Enrollment created successfully.", typeof(EnrollmentDetailDto))]
        [SwaggerResponse(400, "Invalid request or duplicate enrollment.")]
        public async Task<ActionResult<EnrollmentDetailDto>> Create([FromBody] CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.EnrollmentId }, result);
        }

        // =========================================================
        // PUT: api/enrollment/{id}
        // =========================================================
        /// <summary>
        /// Updates an enrollment (status, grade, or reason).
        /// </summary>
        /// <remarks>
        /// **Example Request:**
        /// ```json
        /// {
        ///   "status": "Dropped",
        ///   "grade": null,
        ///   "statusChangeReason": "Dropped due to repeated absence"
        /// }
        /// ```
        /// </remarks>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(EnrollmentDetailDto), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(200, "Enrollment updated successfully.", typeof(EnrollmentDetailDto))]
        [SwaggerResponse(404, "Enrollment not found.")]
        public async Task<ActionResult<EnrollmentDetailDto>> Update(Guid id, [FromBody] UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // =========================================================
        // DELETE: api/enrollment/{id}
        // =========================================================
        /// <summary>
        /// Deletes an enrollment by ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerResponse(204, "Enrollment deleted successfully.")]
        [SwaggerResponse(404, "Enrollment not found.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
