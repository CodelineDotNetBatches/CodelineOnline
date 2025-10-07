using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
namespace ReportsManagements.Controllers
{
    // API controller for managing reason codes
    [Route("reasons")]
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
        [HttpGet("all")]
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
                return NotFound(new ErrorResponse { Status = 404, Code = "NOT_FOUND", Message = "Reason code not found" });
            return Ok(item);
        }
        // POST: api/reasons
        // Creates a new reason code
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Models.ReasonCode reasonCode)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse { Status = 400, Code = "INVALID_MODEL", Message = "Invalid data format", Details = ModelState });
            var all = await _repository.GetAllAsync();
            if (all.Any(rc => rc.Code.Equals(reasonCode.Code, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new ErrorResponse { Status = 400, Code = "DUPLICATE_CODE", Message = "Reason code already exists" });
            }
            await _repository.AddAsync(reasonCode);
            return CreatedAtAction(nameof(GetById), new { id = reasonCode.ReasonCodeId }, reasonCode);
        }
        // PUT: api/reasons/{id}
        // Updates an existing reason code
        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.ReasonCode reasonCode)
        {
            if (id != reasonCode.ReasonCodeId)
                return BadRequest(new ErrorResponse { Status = 400, Code = "ID_MISMATCH", Message = "ID in URL does not match body" });
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return NotFound(new ErrorResponse { Status = 404, Code = "NOT_FOUND", Message = "Reason code not found" });
            await _repository.UpdateAsync(reasonCode);
            return NoContent();
        }
        // DELETE: api/reasons/{id}
        // Deletes a reason code by ID
        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null)
                return NotFound(new ErrorResponse { Status = 404, Code = "NOT_FOUND", Message = "Reason code not found" });
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}