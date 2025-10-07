using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CoursesManagement.Services;
using CoursesManagement.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace CoursesManagement.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // =========================================================
        // GET ALL
        // =========================================================
        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "List of all categories retrieved successfully.", typeof(IEnumerable<CategoryDto>))]
        [SwaggerResponse(500, "Server error occurred.")]

        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _service.GetAllCategoriesAsync();
                if (!categories.Any())
                    return NotFound(new { message = "No categories found." });

                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving categories: {ex.Message}" });
            }
        }

        // =========================================================
        // GET CATEGORY (BY ID or NAME)
        // =========================================================
        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(CategoryDetailDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "Category details retrieved successfully.", typeof(CategoryDetailDto))]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]

        public async Task<IActionResult> GetCategory([FromQuery] Guid? id = null, [FromQuery] string? name = null)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "Provide either Category ID or Name." });

            try
            {
                var category = await _service.GetCategoryAsync(id, name);
                if (category == null)
                    return NotFound(new { message = "Category not found." });

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving category: {ex.Message}" });
            }
        }

        // =========================================================
        // GET COURSES BY CATEGORY (ID or NAME)
        // =========================================================
        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CourseListDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "Courses retrieved successfully for this category.", typeof(IEnumerable<CourseListDto>))]
        [SwaggerResponse(404, "Courses not found.")]
        [SwaggerResponse(500, "Server error occurred.")]

        public async Task<IActionResult> GetCoursesByCategory([FromQuery] Guid? id = null, [FromQuery] string? name = null)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "Provide either ID or Name." });

            try
            {
                var courses = await _service.GetCoursesByCategoryAsync(id, name);
                if (courses == null || !courses.Any())
                    return NotFound(new { message = "No courses found for this category." });

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving courses: {ex.Message}" });
            }
        }

        // =========================================================
        // GET PROGRAMS BY CATEGORY (ID or NAME)
        // =========================================================
        [HttpGet]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ProgramDetailsDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "Programs retrieved successfully for this category.", typeof(IEnumerable<ProgramDetailsDto>))]
        [SwaggerResponse(404, "Programs not found.")]
        [SwaggerResponse(500, "Server error occurred.")]

        public async Task<IActionResult> GetProgramsByCategory([FromQuery] Guid? id = null, [FromQuery] string? name = null)
        {
            if (id == null && string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "Provide either ID or Name." });

            try
            {
                var programs = await _service.GetProgramsByCategoryAsync(id, name);
                if (programs == null || !programs.Any())
                    return NotFound(new { message = "No programs found for this category." });

                return Ok(programs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving programs: {ex.Message}" });
            }
        }

        // =========================================================
        // CREATE
        // =========================================================
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerResponse(201, "Category created successfully.", typeof(CategoryDto))]
        [SwaggerResponse(400, "Invalid input data.")]
        [SwaggerResponse(500, "Server error occurred.")]

        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var created = await _service.CreateCategoryAsync(dto);
                return CreatedAtAction(nameof(GetCategory), new { id = created.CategoryId }, created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error creating category: {ex.Message}" });
            }
        }

        // =========================================================
        // UPDATE
        // =========================================================
        [HttpPut]
        //[Authorize(Roles = "Admin,Instructor")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [SwaggerResponse(204, "Category updated successfully.")]
        [SwaggerResponse(400, "Invalid input data.")]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> UpdateCategory([FromQuery] Guid id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateCategoryAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error updating category: {ex.Message}" });
            }
        }

        // =========================================================
        // DELETE
        // =========================================================
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [SwaggerResponse(204, "Category deleted successfully.")]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid id)
        {
            try
            {
                await _service.DeleteCategoryAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error deleting category: {ex.Message}" });
            }
        }
    }
}
