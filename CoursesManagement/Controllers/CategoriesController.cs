using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CoursesManagement.Services;
using CoursesManagement.DTOs;

namespace CoursesManagement.Controllers
{
    /// <summary>
    /// Manages CRUD operations for categories.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // =========================================================
        // GET: api/category
        // =========================================================
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [SwaggerResponse(200, "List of all categories retrieved successfully.", typeof(IEnumerable<CategoryDto>))]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _service.GetAllCategoriesAsync();
            return Ok(result);
        }

        // =========================================================
        // GET: api/category/{id}
        // =========================================================
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDetailDto), 200)]
        [ProducesResponseType(404)]
        [SwaggerResponse(200, "Category details retrieved successfully.", typeof(CategoryDetailDto))]
        [SwaggerResponse(404, "Category not found.")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _service.GetCategoryByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // =========================================================
        // POST: api/category
        // =========================================================
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(400)]
        [SwaggerResponse(201, "Category created successfully.", typeof(CategoryDto))]
        [SwaggerResponse(400, "Invalid request data or duplicate category.")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _service.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = result.CategoryId }, result);
        }

        // =========================================================
        // PUT: api/category/{id}
        // =========================================================
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerResponse(204, "Category updated successfully.")]
        [SwaggerResponse(404, "Category not found.")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateCategoryAsync(id, dto);
            return NoContent();
        }

        // =========================================================
        // DELETE: api/category/{id}
        // =========================================================
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [SwaggerResponse(204, "Category deleted successfully.")]
        [SwaggerResponse(404, "Category not found.")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _service.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
