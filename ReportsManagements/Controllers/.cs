using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Repositories;

namespace ReportsManagements.Controllers
{
    [Route("api/[files]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorage _repository;

        public FileStorageController(IFileStorage fileStorageRepository)
        {
            _repository = fileStorageRepository;
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
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.FileStorage file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(file);
            return CreatedAtAction(nameof(GetById), new { id = file.FileStorageId }, file);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.FileStorage file)
        {
            if (id != file.FileStorageId) return BadRequest();
            await _repository.UpdateAsync(file);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
