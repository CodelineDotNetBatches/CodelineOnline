using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Repositories;


namespace ReportsManagements.Controllers
{
    // API controller for managing reason codes
    [Route("api/reasons")]
    [ApiController]

    public class ReasonCodeController : ControllerBase
    {
        // Dependency on the reason code repository
        private readonly IReasonCodeRepository _repository;
        public ReasonCodeController(IReasonCodeRepository reasonCodeRepository)
        {
            _repository = reasonCodeRepository;
        }

        // GET: api/reasons
        // Retrieves all reason codes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        // GET: api/reasons/{id}
        // Retrieves a specific reason code by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        // POST: api/reasons
        // Creates a new reason code
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.ReasonCode reasonCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(reasonCode);
            return CreatedAtAction(nameof(GetById), new { id = reasonCode.ReasonCodeId }, reasonCode);
        }

        // PUT: api/reasons/{id}
        // Updates an existing reason code
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.ReasonCode reasonCode) 
        {
            if (id != reasonCode.ReasonCodeId) return BadRequest();

            await _repository.UpdateAsync(reasonCode);
            return NoContent();
        }

        // DELETE: api/reasons/{id}
        // Deletes a reason code by ID
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
