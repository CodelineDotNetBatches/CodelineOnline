using Microsoft.AspNetCore.Mvc;
using ReportsManagements.DTOs;
using ReportsManagements.Models;
using ReportsManagements.Services;
using static ReportsManagements.DTOs.BranchDtos;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("branch")]
    public class BranchController : ControllerBase
    {
        private readonly BranchService _service;

        public BranchController(BranchService service)
        {
            _service = service;
        }

        [HttpGet("AllBranchs")]
        public async Task<IActionResult> Get()
        {
            var branches = await _service.GetAllBranchesAsync();
            var result = branches.Select(b => new BranchResponseDto
            {
                BranchId = b.BranchId,
                Name = b.Name,
                IsActive = b.IsActive
            });
            return Ok(result);
        }

        [HttpGet("{id}/Details")]
        public async Task<IActionResult> Get(int id)
        {
            var branch = await _service.GetBranchByIdAsync(id);
            if (branch == null)
                return NotFound($"Branch with ID:{id} not found");

            return Ok(new BranchDtos.BranchResponseDto
            {
                BranchId = branch.BranchId,
                Name = branch.Name,
                IsActive = branch.IsActive
            });
        }



        
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] BranchCreateDto dto)
        {
            var created = await _service.CreateBranchAsync(dto.Name, dto.Address);

            return CreatedAtAction(nameof(Get), new { id = created.BranchId }, new BranchResponseDto
            {
                BranchId = created.BranchId,
                Name = created.Name,
                IsActive = created.IsActive
            });

        }


        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Put(int id, [FromBody] BranchDtos.BranchUpdateDto dto)
        {
            var branch = await _service.GetBranchByIdAsync(id);
            if (branch == null)
                return NotFound($"Branch with ID:{id} not found");

            branch.Name = dto.Name;
            branch.Address = dto.Address;
            branch.IsActive = dto.IsActive;

            var updated = await _service.UpdateBranchAsync(id, branch);
            return Ok(updated);
        }




        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteBranchAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/Geolocations")]
        public async Task<IActionResult> GetBranchGeolocations(int id)
        {
            var geolocations = await _service.GetBranchGeolocationsAsync(id);
            if (geolocations == null || !geolocations.Any())
                return NotFound($"No geolocations found for Branch with ID:{id}");

            return Ok(geolocations);
        }
    }
}
