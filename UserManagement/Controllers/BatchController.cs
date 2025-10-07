using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Services;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _service;

        public BatchController(IBatchService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // ============================================================
        // BASIC CRUD
        // ============================================================

        /// <summary>
        /// Get all batches.
        /// </summary>
        [HttpGet("GetAllBatch")]
        public async Task<IActionResult> GetAll()
        {
            var batches = await _service.GetAllBatchesAsync();
            return Ok(batches);
        }

        /// <summary>
        /// Get batch by ID.
        /// </summary>
        [HttpGet("GetBatchById")]
        public async Task<IActionResult> GetById(int id)
        {
            var batch = await _service.GetBatchByIdAsync(id);
            if (batch == null) return NotFound($"Batch with ID {id} not found.");
            return Ok(batch);
        }

        /// <summary>
        /// Create a new batch.
        /// </summary>
        [HttpPost("CreateBatch")]
        public async Task<IActionResult> Create([FromBody] BatchDTO dto)
        {
            var created = await _service.CreateBatchAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BatchId }, created);
        }

        /// <summary>
        /// Update a batch.
        /// </summary>
        [HttpPut("UpdateBatchById")]
        public async Task<IActionResult> Update(int id, [FromBody] BatchDTO dto)
        {
            if (id != dto.BatchId) return BadRequest("Batch ID mismatch.");

            var updated = await _service.UpdateBatchAsync(dto);
            return Ok(updated);
        }

        /// <summary>
        /// Delete a batch.
        /// </summary>
        [HttpDelete("DeleteBatchById")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteBatchAsync(id);
            return NoContent();
        }

        // ============================================================
        // FILTERING ENDPOINTS
        // ============================================================

        /// <summary>
        /// Filter batches by name (case-insensitive contains).
        /// Example: /Batch/FilterByName?name=summer
        /// </summary>
        [HttpGet("FilterByName")]
        public async Task<IActionResult> FilterByName([FromQuery] string name)
        {
            var batches = await _service.GetAllBatchesAsync();
            var filtered = batches
                .Where(b => b.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter batches by status (e.g., Active, Completed, Pending).
        /// Example: /Batch/FilterByStatus?status=Active
        /// </summary>
        [HttpGet("FilterByStatus")]
        public async Task<IActionResult> FilterByStatus([FromQuery] string status)
        {
            var batches = await _service.GetAllBatchesAsync();
            var filtered = batches
                .Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter batches by start date range.
        /// Example: /Batch/FilterByStartDate?from=2025-01-01&to=2025-03-01
        /// </summary>
        [HttpGet("FilterByStartDate")]
        public async Task<IActionResult> FilterByStartDate([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var batches = await _service.GetAllBatchesAsync();
            var filtered = batches
                .Where(b =>
                    (!from.HasValue || b.StartDate >= from.Value) &&
                    (!to.HasValue || b.StartDate <= to.Value))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter batches by end date range.
        /// Example: /Batch/FilterByEndDate?from=2025-04-01&to=2025-06-30
        /// </summary>
        [HttpGet("FilterByEndDate")]
        public async Task<IActionResult> FilterByEndDate([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var batches = await _service.GetAllBatchesAsync();
            var filtered = batches
                .Where(b =>
                    (!from.HasValue || b.EndDate >= from.Value) &&
                    (!to.HasValue || b.EndDate <= to.Value))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Filter batches by timeline (case-insensitive match).
        /// Example: /Batch/FilterByTimeline?timeline=Phase1
        /// </summary>
        [HttpGet("FilterByTimeline")]
        public async Task<IActionResult> FilterByTimeline([FromQuery] string timeline)
        {
            var batches = await _service.GetAllBatchesAsync();
            var filtered = batches
                .Where(b => !string.IsNullOrEmpty(b.Timeline) &&
                            b.Timeline.Contains(timeline, StringComparison.OrdinalIgnoreCase))
                .ToList();
            return Ok(filtered);
        }

        /// <summary>
        /// Combined filter by multiple criteria.
        /// Example: /Batch/FilterCombined?name=Summer&status=Active&from=2025-01-01&to=2025-05-01&timeline=Phase1
        /// </summary>
        [HttpGet("FilterCombined")]
        public async Task<IActionResult> FilterCombined(
            [FromQuery] string? name,
            [FromQuery] string? status,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? timeline)
        {
            var batches = await _service.GetAllBatchesAsync();

            var filtered = batches.Where(b =>
                (string.IsNullOrEmpty(name) || b.Name.Contains(name, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(status) || b.Status.Equals(status, StringComparison.OrdinalIgnoreCase)) &&
                (!from.HasValue || b.StartDate >= from.Value) &&
                (!to.HasValue || b.EndDate <= to.Value) &&
                (string.IsNullOrEmpty(timeline) || (!string.IsNullOrEmpty(b.Timeline) &&
                 b.Timeline.Contains(timeline, StringComparison.OrdinalIgnoreCase)))
            ).ToList();

            return Ok(filtered);
        }
    }
}
