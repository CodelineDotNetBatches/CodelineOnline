using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AuthenticationManagement.DTOs;
using AuthenticationManagement.Services;

namespace AuthenticationManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;
        public AuthController(IAuthService auth) { _auth = auth; }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _auth.RegisterAsync(dto);
            return result == null ? Conflict("Email already exists.") : Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _auth.LoginAsync(dto);
            return result == null ? Unauthorized("Invalid credentials.") : Ok(result);
        }

        // Example protected endpoint
        [HttpGet("me")]
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]// hide this api from user

        public IActionResult Me()
        {
            return Ok(new
            {
                Name = User.Identity?.Name,
                Email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                Roles = User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList()
            });
        }
    }
}
