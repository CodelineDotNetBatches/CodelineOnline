using System.ComponentModel.DataAnnotations;

namespace UserManagement.DTOs
{
    // DTO for transferring Admin Profile data between layers (Service, Controll
    public class AdminProfileDTO
    {
        // Unique idenifier Admin Profile data between layers (Service , Controller , etc)

        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        // Full  name of the Admin 

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters")]
        public string Name { get; set; }
    }
}
