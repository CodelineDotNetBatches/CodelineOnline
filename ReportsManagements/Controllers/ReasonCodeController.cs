using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Repositories;


namespace ReportsManagements.Controllers
{
    [Route("api/[reasons]")]
    [ApiController]

    public class ReasonCodeController : ControllerBase
    {
        private readonly IReasonCode _repository;
        public ReasonCodeController(IReasonCode reasonCodeRepository)
        {
            _repository = reasonCodeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.ReasonCode reasonCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(reasonCode);
            return CreatedAtAction(nameof(GetById), new { id = reasonCode.ReasonCodeId }, reasonCode);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.ReasonCode reasonCode)
        {
            if (id != reasonCode.ReasonCodeId) return BadRequest();

            await _repository.UpdateAsync(reasonCode);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
