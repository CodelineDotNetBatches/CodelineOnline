using Microsoft.AspNetCore.Mvc;
using ReportsManagements.Models;
using ReportsManagements.Repositories;
using ReportsManagements.Services;
using ReportsManagements.DTOs;
using System.Threading.RateLimiting;

namespace ReportsManagements.Controllers
{
    // API controller for managing file storage operations
    [Route("files")]
    [ApiController] 
    public class FileStorageController : ControllerBase
    {
        // Dependency on the file storage repository
        private readonly IFileStorageRepository _repository;
        private readonly IFileCodeService _fileCodeService;
        private readonly UploadRateLimiterService _rateLimiter;


        public FileStorageController(IFileStorageRepository fileStorageRepository, IFileCodeService fileCodeService, UploadRateLimiterService rateLimiter)
        {
            _repository = fileStorageRepository;
            _fileCodeService = fileCodeService;
            _rateLimiter = rateLimiter;
        }

        // GET: api/files
        // Retrieves all file storage records
        [HttpGet("view-all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _repository.GetAllAsync();
            return Ok(list);
        }

        // GET: api/files/{id}
        // Retrieves a specific file storage record by ID
        [HttpGet("view/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        // POST: api/files
        // Creates a new file storage record
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] FileStorage file)
        {
            string userKey = User.Identity?.Name ?? HttpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

            if (!_rateLimiter.CanUpload(userKey))
            {
                return BadRequest(new ErrorResponse
                {
                    Status = 429,
                    Code = "RATE_LIMIT_EXCEEDED",
                    Message = "Upload limit exceeded. Try again later."
                });
            }

            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Code = "INVALID_MODEL",
                    Message = "Invalid data format",
                    Details = ModelState
                });

            if (!_fileCodeService.IsValidFile(file.FileName, file.FileSize))
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Code = "BadRequest",
                    Message = "File type or size not allowed",
                    Details = new { file.FileName, file.FileSize }
                });

            await _repository.AddAsync(file);
            return CreatedAtAction(nameof(GetById), new { id = file.FileStorageId }, file);
        }

        // PUT: api/files/{id}
        // Updates an existing file storage record
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FileStorage file)
        {
            if (id != file.FileStorageId) return BadRequest();
            await _repository.UpdateAsync(file);
            return NoContent();
        }

        // DELETE: api/files/{id}
        // Deletes a file storage record by ID
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/files/presign
        // Generates a presigned URL for file upload 
        [HttpPost("presign-url")]
        public IActionResult Presign([FromBody] FileDto fileDto)
        {
            if (!_fileCodeService.IsValidFile(fileDto.FileName, fileDto.FileSize))
                return BadRequest(new ErrorResponse
                {
                    Status = 400,
                    Code = "INVALID_FILE_METADATA",
                    Message = "Invalid file metadata",
                    Details = new { fileDto.FileName, fileDto.FileSize }
                });

            // Placeholder for presign logic
            var dummyUrl = $"https://dummy-storage.com/upload/{Guid.NewGuid()}-{fileDto.FileName}";

            return Ok(new
            {
                url = dummyUrl,
                fileDto.FileName,
                fileDto.FileSize,
                fileDto.UploadedBy
            });
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var file = await _repository.GetByIdAsync(id);
            if (file == null) return NotFound();
            return Redirect(file.Url);
        }

 
    }
}
