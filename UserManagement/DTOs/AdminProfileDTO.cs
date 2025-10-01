using System.ComponentModel.DataAnnotations;

namespace UserManagement.DTOs
{
    // DTO for transferring Admin Profile data between layers (Service, Controll
    public class AdminProfileDTO
    {
        // Unique idenifier Admin Profile data between layers (Service , Controller , etc)

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        
    }
}
