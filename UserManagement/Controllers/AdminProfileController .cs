using Microsoft.AspNetCore.Mvc;
using UserManagement.DTOs;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminProfileController : ControllerBase
    {
        private readonly IAdminProfileService _service;

        public AdminProfileController(IAdminProfileService service)
        {
            _service = service;
        }

        // -------------------
        // SYNC ENDPOINTS
        // -------------------

        [HttpGet("GetAllAdmin")]
        public ActionResult<IEnumerable<Admin_Profile>> GetAllAdmins()
        {
            var admins = _service.GetAllAdmins();
            return Ok(admins);
        }

        [HttpGet("GetAdminById/{id}")]
        public ActionResult<Admin_Profile> GetAdminById(int id)
        {
            var admin = _service.GetAdminById(id);
            if (admin == null)
                return NotFound($"Admin with ID {id} not found.");
            return Ok(admin);
        }

        [HttpPost("AddAdmin")]
        public IActionResult AddAdmin([FromBody] AdminProfileDTO adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.AddAdminProfile(adminDto);
            return CreatedAtAction(nameof(GetAdminById), new { id = adminDto.AdminId }, adminDto);
        }

        // -------------------
        // UPDATE ADMIN (PUT)
        // -------------------
        [HttpPut("UpdateAdmin/{id}")]
        public IActionResult UpdateAdmin(int id, [FromBody] AdminProfileDTO adminDto)
        {
            if (id != adminDto.AdminId)
                return BadRequest("ID mismatch.");

            var updated = _service.UpdateAdminProfile(adminDto);
            if (!updated)
                return NotFound($"Admin with ID {id} not found.");

            return Ok($"Admin with ID {id} updated successfully.");
        }

        // -------------------
        // DELETE ADMIN
        // -------------------
        [HttpDelete("DeleteAdmin/{id}")]
        public IActionResult DeleteAdmin(int id)
        {
            var deleted = _service.DeleteAdminProfile(id);
            if (!deleted)
                return NotFound($"Admin with ID {id} not found.");

            return Ok($"Admin with ID {id} deleted successfully.");
        }
    }
}
