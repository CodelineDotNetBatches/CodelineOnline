using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Repositories;

namespace ReportsManagements.Controllers
{
    // API controller for managing file storage operations
    [Route("api/files")]
    [ApiController] 
    public class FileStorageController : ControllerBase
    {
        // Dependency on the file storage repository
        private readonly IFileStorageRepository _repository;

        public FileStorageController(IFileStorageRepository fileStorageRepository)
        {
            _repository = fileStorageRepository;
        }

        // GET: api/files
        // Retrieves all file storage records
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        // GET: api/files/{id}
        // Retrieves a specific file storage record by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // POST: api/files
        // Creates a new file storage record
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.FileStorage file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.AddAsync(file);
            return CreatedAtAction(nameof(GetById), new { id = file.FileStorageId }, file);
        }

        // PUT: api/files/{id}
        // Updates an existing file storage record
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.FileStorage file)
        {
            if (id != file.FileStorageId) return BadRequest();
            await _repository.UpdateAsync(file);
            return NoContent();
        }

        // DELETE: api/files/{id}
        // Deletes a file storage record by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/files/presign
        // Generates a presigned URL for file upload 
        [HttpPost("presign")]
        public IActionResult Presign()
        {
            // Placeholder for presign logic
            var dummyUrl = "https://dummy-storage.com/upload/fake-presigned-url";

            return Ok(new { url = dummyUrl }); // Return the dummy presigned URL
        }

    }
}
