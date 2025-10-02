using Microsoft.AspNetCore.Mvc;
using UserManagement.Models;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]                     // Marks this class as an API controller 
    [Route("api/[controller]")]        // Defines the base route -> API/AdminProfile 
    public class AdminProfileController : ControllerBase
    {
        private readonly AdminProfileService _service;  // Reference to service Layer 

        // Constructor injection: The service is injected by dependency injection (DI)
        public AdminProfileController(AdminProfileService service)
        {
            _service = service; // Store reference for later use
        }

        // -------------------
        // SYNC ENDPOINTS
        // -------------------

        //Get all Admin Profile (sync)

        [HttpGet("sync/all")]        // GET api/AdminProfile/sync/all 

        public ActionResult<IQueryable<Admin_Profile>> GetAllAdmins()
        {
            var admins = _service.GetAllAdmins();      // Call service to get admins
            return Ok(admins);                         // Return 200 OK with data
        }


        // Get Admin Profile by Id (sync)

        [HttpGet("sync/{id}")]                    // GET api/AdminProfile/sync/5

        public ActionResult<Admin_Profile> GetAdminById(int id)
        {
            var admin = _service.GetAdminById(id);       // Call service 
            if (admin == null)                          // if Not found 
                return NotFound();                      // return 404 
            return Ok(admin);                           // return 200 OK 

        }


        // Add new Admine Profile (sync)

        [HttpPost("sync/add")]                 // POST api/Admin Profile /sync /add 

        public IActionResult AddAdmin([FromBody] Admin_Profile admin)
        {
            _service.AddAdmin(admin);                  // Call service to add admin
            return CreatedAtAction(nameof(GetAdminById), new { id = admin.AdminId }, admin); // Returns 201 Created with location header pointing to new admin 
        }

        // -------------------
        // ASYNC ENDPOINTS
        // -------------------


        // Get all Admin Profile (async)

        [HttpGet("async/all")]             // GET api/AdminProfile/async/all
        public async Task<ActionResult<IEnumerable<Admin_Profile>>> GetAllAdminsAsync()
        {
            var admins = await _service.GetAllAdminsAsync(); // Call async service method
            return Ok(admins);                         // Return 200 OK
        }

        // Get AdminProfile by Id (async) 

        [HttpGet("async/{id}")]                // GET api/Admin Profile/async/5 
        public async Task<ActionResult<Admin_Profile>> GetAdminByIdAsync(int id)
        {
            var admin = await _service.GetAdminByIdAsync(id); // Call async service method
            if (admin == null)                         // If not found
                return NotFound();                     // Return 404
            return Ok(admin);                          // Return 200 OK
        }


        // Add new AdminProfile (async)

        [HttpPost("async/add")]                        // POST api/AdminProfile/async/add
                                                      
        public async Task<IActionResult> AddAdminAsync([FromBody] Admin_Profile admin)
        {
            await _service.AddAdminAsync(admin);       // Call async service to add admin
            return CreatedAtAction(nameof(GetAdminByIdAsync), new { id = admin.AdminId }, admin);   // Returns 201 Created with location header pointing to new admin
        }
    }
}
