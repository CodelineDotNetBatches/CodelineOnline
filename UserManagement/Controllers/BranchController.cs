using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.DTOs;
using UserManagement.Services;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _service;

        public BranchesController(IBranchService service)
        {
            _service = service;
        }

        // GET: api/branches

        [HttpGet("GetAllBranches")]
        [ProducesResponseType(typeof(IEnumerable<BranchDTO>), 200)]
        public async Task<ActionResult<IEnumerable<BranchDTO>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(data);
        }

        // GET: api/branches/5
        [HttpGet("GetBranchByID")]
        [ProducesResponseType(typeof(BranchDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BranchDTO>> GetById(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        // GET: api/branches/with-more-than-three-batches
        [HttpGet("GetBranchesWithMoreThanThreeBatches")]
        [ProducesResponseType(typeof(IEnumerable<BranchDTO>), 200)]
        public async Task<ActionResult<IEnumerable<BranchDTO>>> GetBranchesWithMoreThanThreeBatches()
        {
            var data = await _service.GetBranchesWithMoreThanThreeBatchesAsync();
            return Ok(data);
        }

        // POST: api/branches
        [HttpPost("Create_Branch")]
        [ProducesResponseType(typeof(BranchDTO), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] BranchDTO dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            await _service.AddAsync(dto);

            
            return CreatedAtRoute(nameof(GetById), new { id = dto.BranchId }, dto);
        }


        // PUT: api/branches/5
        [HttpPut("Update_Branch")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(int id, [FromBody] BranchDTO dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            try
            {
                await _service.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE: api/branches/5
        [HttpDelete("Delete_Branch")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
