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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound(new { message = "Course not found" });

            return Ok(course);
        }

        [HttpGet("category/{categoryId:int}")]
        public async Task<IActionResult> GetByCategory(Guid categoryId)
        {
            var courses = await _courseService.GetCoursesByCategoryAsync(categoryId);
            return Ok(courses);
        }

        [HttpGet("level/{level}")]
        public async Task<IActionResult> GetByLevel(LevelType level)
        {
            var courses = await _courseService.GetCoursesByLevelAsync(level);
            return Ok(courses);
        }

        [HttpGet("{id:guid}/category")]
        public async Task<IActionResult> GetCourseWithCategory(Guid id)
        {
            var course = await _courseService.GetCourseWithCategoryAsync(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // --- CREATE using CourseCreateDto ---
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CourseCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var course = await _courseService.AddCourseAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
        }

        // --- UPDATE using CourseUpdateDto ---
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CourseUpdateDto dto)
        {
            if (id != dto.CourseId)
                return BadRequest("Course ID mismatch.");

            var updatedCourse = await _courseService.UpdateCourseAsync(dto);
            if (updatedCourse == null)
                return NotFound(new { message = "Course not found" });

            return Ok(updatedCourse);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }
}
