using AuthenticationManagement.DTOs;
using AuthenticationManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetAllUser/")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("GetUserByID/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _service.GetByIdAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("CreateUser/")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var user = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.UserID }, user);
        }

        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
