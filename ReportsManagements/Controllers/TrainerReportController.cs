using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Services;
using static ReportsManagements.DTOs.TrainerReportDtos;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/v1/reports/trainer")]
    public class TrainerReportController : ControllerBase
    {
        private readonly TrainerReportService _service;

        public TrainerReportController(TrainerReportService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _service.GetAllAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _service.GetByIdAsync(id);
            if (report == null)
                return NotFound(new { Message = "Trainer report not found" });

            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TrainerReportCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TrainerReportId }, created);
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] TrainerReportUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { Message = "Trainer report not found" });

            return NoContent();
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { Message = "Trainer report not found" });

            return NoContent();
        }
    }
}
