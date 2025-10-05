using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Models;
using ReportsManagements.Repositories;

namespace ReportsManagements.Controllers
{
    [ApiController]
    [Route("api/v1/branch")]
    public class BranchController : ControllerBase
    {
        private readonly IBranchRepository _repo;


        public BranchController(IBranchRepository repo)
        {
            _repo = repo;
        }
        //Retrieve all branches 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var branches = await _repo.GetAllAsync();

            return Ok(branches);
        }

        //Retrieve branch by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> Get(int id)
        {
            var branch = await _repo.GetByIdAsync(id);
            if (branch == null)
                return NotFound($"Branch with ID:{id} not found");

            return Ok(branch);
        }

        //Create a new branch and return the created branch
        [HttpPost]
        public async Task<ActionResult<Branch>> Post(Branch branch)
        {
            var created = await _repo.AddAsync(branch);
            return CreatedAtAction(nameof(Get), new { id = created.BranchId }, created);
        }

        //Update an existing branch if the provided id matches the branch id
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Branch branch)
        {
            if (id != branch.BranchId)
                return BadRequest("Branch Id dose not match.");

            var update = await _repo.UpdateAsync(branch);
            if (update == null)
                return NotFound($"Branch with ID:{id} not found");

            return Ok(update);
        }
        //delete a branch by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Branch with ID:{id} not found");
            await _repo.DeleteAsync(id);
            return NoContent();
        }


        [HttpGet("{id}/geolocations")]
        public async Task<IActionResult> GetBranchGeolocations(int id)
        {
            var geolocations = await _repo.GetBranchGeolocationsAsync(id);

            if (geolocations == null || !geolocations.Any())
                return NotFound($"No geolocations found for Branch with ID:{id}");

            return Ok(geolocations);

        }

        [HttpGet("{id}/report")]
        public async Task<IActionResult> GetBranchReport(int id)
        {
            var branch = await _repo.GetByIdAsync(id);
            if (branch == null)
                return NotFound($"Branch with ID:{id} not found");
            var report = new
            {
                BranchId = branch.BranchId,
                BranchName = branch.Name,

                Geolocations = await _repo.GetBranchGeolocationsAsync(id)
            };

            return Ok(report);
        }
    }
}
