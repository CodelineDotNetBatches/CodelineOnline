using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _service;

        public BatchController(IBatchService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all batches.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var batches = await _service.GetAllBatchesAsync();
            return Ok(batches);
        }

        /// <summary>
        /// Get batch by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var batch = await _service.GetBatchByIdAsync(id);
            if (batch == null) return NotFound();
            return Ok(batch);
        }

        /// <summary>
        /// Create a new batch.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BatchDTO dto)
        {
            var created = await _service.CreateBatchAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BatchId }, created);
        }

        /// <summary>
        /// Update a batch.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BatchDTO dto)
        {
            if (id != dto.BatchId) return BadRequest();

            var updated = await _service.UpdateBatchAsync(dto);
            return Ok(updated);
        }

        /// <summary>
        /// Delete a batch.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteBatchAsync(id);
            return NoContent();
        }
    }
}
