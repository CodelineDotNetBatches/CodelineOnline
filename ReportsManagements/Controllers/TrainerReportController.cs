using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Repositories;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("reports/trainer")]
    public class TrainerReportController: ControllerBase
    {
        private readonly TrainerReportRepository _repo;
        public TrainerReportController(TrainerReportRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reports = await _repo.GetAllAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var report = await _repo.GetByIdAsync(id);
            if (report == null)
                return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.TrainerReport report)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = await _repo.AddAsync(report);
            return CreatedAtAction(nameof(GetById), new { id = created.TrainerReportId }, created);

        }

        private object GetById()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Models.TrainerReport report)
        {
            if (id != report.TrainerReportId)
                return BadRequest("ID mismatch");
            var updated = await _repo.UpdateAsync(report);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }



    }
}
