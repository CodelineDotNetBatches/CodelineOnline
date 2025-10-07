using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public class Branch
    {
        [Key]
      
        public int BranchId { get; set; } // Primary Key for the Branch table

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
        public string City { get; set; } // The city where the branch is located

    }
}
