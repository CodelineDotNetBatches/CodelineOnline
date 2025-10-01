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

        }


}
