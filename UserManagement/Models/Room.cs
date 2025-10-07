using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Room
    {

        [Required(ErrorMessage = "Room number is required.")]
        [StringLength(10, ErrorMessage = "Room number cannot exceed 10 characters.")]
        public string RoomNumber { get; set; } // Unique room number 


    }
}
