using CoursesManagement.DTOs;
using CoursesManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoursesManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CertificatesController: ControllerBase 
    {
        private readonly ICertificateService _service;

        public CertificatesController(ICertificateService service)
        {
            _service = service;
        }

        // Issue a certificate for a user/course

        [HttpPost]
        public async Task<ActionResult<CertificateDetailsDto>> Issue([FromBody] CertificateIssueDto dto)
        {
            var result = await _service.IssueAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.CertificateId }, result);
        }

        // Get a certificate by id
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CertificateDetailsDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        // List certificates for a user
        [HttpGet("by-user/{userId:guid}")]
        public async Task<ActionResult<List<CertificateListItemDto>>> ListByUser(Guid userId)
        {
            var items = await _service.ListByUserAsync(userId);
            return Ok(items);
        }

        // Search with paging/filtering 
        //[HttpGet]
        //public async Task<ActionResult<PagedResult<CertificateListItemDto>>> Search([FromQuery] CertificateQueryDto query)
        //{
        //    var result = await _service.SearchAsync(query);
        //    return Ok(result);
        //}

        // Verify by URL (exact match)
        [HttpGet("verify/by-url")]
        public async Task<ActionResult<CertificateVerifyResultDto>> VerifyByUrl([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return BadRequest("url is required.");
            var result = await _service.VerifyByUrlAsync(url);
            if (!result.Found) return NotFound(result);
            return Ok(result);
        }

        // Verify by enrollment id
        [HttpGet("verify/by-enrollment/{enrollmentId:guid}")]
        public async Task<ActionResult<CertificateVerifyResultDto>> VerifyByEnrollment(Guid enrollmentId)
        {
            var result = await _service.VerifyByEnrollmentAsync(enrollmentId);
            if (!result.Found) return NotFound(result);
            return Ok(result);
        }

        // Update certificate URL
        [HttpPut("{id:guid}/url")]
        public async Task<IActionResult> UpdateUrl(Guid id, [FromBody] CertificateUpdateUrlDto dto)
        {
            await _service.UpdateUrlAsync(id, dto);
            return NoContent();
        }

        // Delete a certificate
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

