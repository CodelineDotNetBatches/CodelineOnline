using System.ComponentModel.DataAnnotations;
using UserManagement.Models;

namespace UserManagement.DTOs
{
    public class RoomDTO
    {
        [Required]
        public string RoomNumber { get; set; } // Primary Key for the Room Number
        [Required]
        public string RoomType { get; set; }
        public string? Description { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int BranchId { get; set; } // Foreign Key linking Room to a Branch

    }
}