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
        }

}
}
