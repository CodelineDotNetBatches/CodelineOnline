using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Room
    {

        [Key]
        public string RoomNumber { get; set; } // Praimary Key for the Room Number 


        [Required(ErrorMessage = "Room type is required.")]
        [StringLength(50, ErrorMessage = "Room type cannot exceed 50 characters.")]
        public string RoomType { get; set; }

    }
}
