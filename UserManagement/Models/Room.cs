using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public class Room
    {

        [Key]
        public string RoomNumber { get; set; } // Praimary Key for the Room Number 


        [Required(ErrorMessage = "Room type is required.")]
        [StringLength(50, ErrorMessage = "Room type cannot exceed 50 characters.")]
        public string RoomType { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Room description cannot exceed 300 characters.")]
        public string? Description { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Room capacity must be between 1 and 10.")]
        public int Capacity { get; set; }


        [ForeignKey("Branch")]
        public int BranchId { get; set; } // Foreign Key linking Room to a Branch

        public Branch branchs { get; set; } // Navigation property

    }
}
