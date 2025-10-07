using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Services;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("branchreport")]
    public class BranchReportController: ControllerBase
    {
        private readonly BranchReportService _service;

        public BranchReportController(BranchReportService service)
        {
            _service = service;
        }

        [HttpGet("AllReports")]
        public async Task<IActionResult> Get()
        {
            var reports = await _service.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}/Details")]
        public async Task<IActionResult> Get(int id)
        {
            var report = await _service.GetReportByIdAsync(id);
            if (report == null)
                return NotFound($"Report with ID:{id} not found");
            return Ok(report);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BranchReportDtos.BranchReportCreateDto dto)
        {
            var created = await _service.CreateReportAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.BranchReportId }, created);
        }

        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Put(int id, [FromBody] BranchReportDtos.BranchReportCreateDto dto)
        {
            var updated = await _service.UpdateReportAsync(id, dto);
            if (updated == null)
                return NotFound($"Report with ID:{id} not found");
            return Ok(updated);
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteReportAsync(id);
            return NoContent();
        }

        [HttpGet("ByBranch/{branchId}")]
        public async Task<IActionResult> GetReportsByBranchId(int branchId)
        {
            var reports = await _service.GetReportsByBranchIdAsync(branchId);
            return Ok(reports);
        }

    }
}
