using CoursesManagement.DTOs;
using CoursesManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagement.Controllers
{
    /// <summary>
    /// API Controller for managing Categories.
    /// Handles HTTP requests and responses.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")] // API Versioning example
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // ======================
        // GET: api/v1/categories
        // ======================
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllCategoriesAsync();
            return Ok(categories); // standardized response
        }

        // ======================
        // GET: api/v1/categories/{id}
        // ======================
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _service.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // ======================
        // POST: api/v1/categories
        // ======================
        [HttpPost]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateCategoryAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CategoryId }, created);
        }

        // ======================
        // PUT: api/v1/categories/{id}
        // ======================
        [HttpPut("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.CategoryId)
                return BadRequest("ID mismatch.");

            await _service.UpdateCategoryAsync(id, dto);
            return NoContent();
        }

        // ======================
        // DELETE: api/v1/categories/{id}
        // ======================
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
