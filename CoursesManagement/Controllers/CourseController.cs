using CoursesManagement.DTOs;
using CoursesManagement.Models;
using CoursesManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("GetCourseById/{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            return Ok(course);
        }

        [HttpGet("GetCourseByCategoryId/{categoryId:Guid}")]
        public async Task<IActionResult> GetByCategory(Guid categoryId)
        {
            var courses = await _courseService.GetCoursesByCategoryAsync(categoryId);
            return Ok(courses);
        }

        [HttpGet("GetCourseBylevel/{level}")]
        public async Task<IActionResult> GetByLevel(LevelType level)
        {
            var courses = await _courseService.GetCoursesByLevelAsync(level);
            return Ok(courses);
        }

        [HttpGet("GetCourseWithCategoryDetails/{id:guid}")]
        public async Task<IActionResult> GetCourseWithCategory(Guid id)
        {
            var course = await _courseService.GetCourseWithCategoryAsync(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // --- CREATE using CourseCreateDto ---
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await _courseService.AddCourseAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
        }

        // --- UPDATE using CourseUpdateDto ---
        [HttpPut("UpdateCourse/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseUpdateDto dto)
        {
            if (id != dto.CourseId)
                return BadRequest("Course ID mismatch.");

            var updatedCourse = await _courseService.UpdateCourseAsync(dto);
            if (updatedCourse == null)
                return NotFound(new { message = "Course not found" });

            return Ok(updatedCourse);
        }

        [HttpDelete("DeleteCourse/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }

        [HttpGet("GetCourseWithEnrollmentList/{id:guid}")]
        public async Task<IActionResult> GetCourseWithEnrollmentList(Guid id)
        {
            var course = await _courseService.GetCourseWithEnrollmentListAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found." });

            // Ensure enrollments are included in response
            return Ok(new
            {
                course.CourseId,
                course.CourseName,
                course.CourseLevel,
                course.Price,
                Enrollments = course.Enrollments?.Select(e => new
                {
                    e.CourseId,
                    e.UserId,
                    e.EnrolledAt
                    // Add more fields if Enrollment has them
                })
            });
        }

    }
}
