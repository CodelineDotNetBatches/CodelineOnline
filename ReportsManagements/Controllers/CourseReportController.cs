using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Repositories;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/course-reports")]
    public class CourseReportController : ControllerBase
    {
        private readonly ICourseReportRepository _repo;

        public CourseReportController(ICourseReportRepository repo)
        {
            _repo = repo;
        }


        [HttpGet("All")]
        public async Task<IActionResult> Get()
        {
            var reports = await _repo.GetAllAsync();
            return Ok(reports);
        }

        [HttpGet("view/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var report = await _repo.GetByIdAsync(id);
            if (report == null)
                return NotFound();
            return Ok(report);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Models.CourseReport report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _repo.AddAsync(report);
            return CreatedAtAction(nameof(GetById), new { id = created.CourseReportId }, created);
        }

        private object GetById()
        {
            throw new NotImplementedException();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Models.CourseReport report)
        {
            if (id != report.CourseReportId)
                return BadRequest("ID mismatch");
            var updated = await _repo.UpdateAsync(report);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }

    }
}
