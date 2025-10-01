using Microsoft.AspNetCore.Mvc;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [ApiController]                     // Marks this class as an API controller 
    [Route("api/[controller]")]        // Defines the base route -> API/AdminProfile 
    public class AdminProfileController : ControllerBase
    {
        private readonly AdminProfileService _service;  // Reference to service Layer 


    }
}
