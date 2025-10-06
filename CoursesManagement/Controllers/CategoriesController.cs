using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CoursesManagement.Services;
using CoursesManagement.DTOs;

namespace CoursesManagement.Controllers
{
    /// <summary>
    /// API Controller that manages CRUD operations and relationships for Categories.
    /// </summary>
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
        // GET BY ID
        // =========================================================
        [HttpGet]
        [SwaggerResponse(200, "Category details retrieved successfully.", typeof(CategoryDetailDto))]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> GetCategoryById([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Category ID is required." });

            try
            {
                var category = await _service.GetCategoryByIdAsync(id);
                if (category == null)
                    return NotFound(new { message = $"No category found with ID: {id}" });

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving category: {ex.Message}" });
            }
        }

        // =========================================================
        // GET BY NAME
        // =========================================================
        [HttpGet]
        [SwaggerResponse(200, "Category retrieved successfully by name.", typeof(CategoryDetailDto))]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> GetCategoryByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "Category name must be provided." });

            try
            {
                var category = await _service.GetCategoryByNameAsync(name);
                if (category == null)
                    return NotFound(new { message = $"No category found with name: {name}" });

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving category by name: {ex.Message}" });
            }
        }

        // =========================================================
        // GET COURSES BY CATEGORY
        // =========================================================
        [HttpGet]
        [SwaggerResponse(200, "Courses retrieved successfully for this category.", typeof(IEnumerable<CourseListDto>))]
        [SwaggerResponse(404, "Category or courses not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> GetCoursesByCategory([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Category ID is required." });

            try
            {
                var courses = await _service.GetCoursesByCategoryAsync(id);
                if (courses == null || !courses.Any())
                    return NotFound(new { message = $"No courses found for category ID: {id}" });

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error retrieving courses: {ex.Message}" });
            }
        }

        // =========================================================
        // GET PROGRAMS BY CATEGORY
        // =========================================================
        [HttpGet]
        [SwaggerResponse(200, "Programs retrieved successfully for this category.", typeof(IEnumerable<ProgramDetailsDto>))]
        [SwaggerResponse(404, "Category or programs not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> GetProgramsByCategory([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Category ID is required." });

            try
            {
                var programs = await _service.GetProgramsByCategoryAsync(id);
                if (programs == null || !programs.Any())
                    return NotFound(new { message = $"No programs found for category ID: {id}" });

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
                return CreatedAtAction(nameof(GetCategoryById), new { id = created.CategoryId }, created);
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
        [SwaggerResponse(204, "Category updated successfully.")]
        [SwaggerResponse(400, "Invalid input data.")]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> UpdateCategory([FromQuery] Guid id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id == Guid.Empty)
                return BadRequest(new { message = "Category ID is required." });

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
        [SwaggerResponse(204, "Category deleted successfully.")]
        [SwaggerResponse(404, "Category not found.")]
        [SwaggerResponse(500, "Server error occurred.")]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Category ID is required." });

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
