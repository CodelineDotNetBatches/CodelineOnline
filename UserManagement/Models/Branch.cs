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

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string Country { get; set; } //The country where the branch is located

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; } //Optional email for branch contact

    }
}
