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

        [Required]
        public string BranchName { get; set; } // The city where the branch is located


        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country name cannot exceed 100 characters.")]
        public string Country { get; set; } //The country where the branch is located

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; } //Optional email for branch contact

        [Required]
        public bool IsActive { get; set; }  // Optional description of the branch


        // nav
        public ICollection<BranchPN> branchPNs { get; set; }
        public ICollection<Room> rooms { get; set; }

        public ICollection<Admin_Profile> Admin_Profiles { get; set; }

        public ICollection<Batch> batches { get; set; }


        public ICollection<Instructor> instructors { get; set; }

        public ICollection<Trainee> trainees { get; set; } 
    }


    public class BranchPN
    {
        [ForeignKey("Branch")]
        public int BranchId { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 digits.")]
        public int PhoneNumber { get; set; }



        //nevegation 
        public Branch branchs { get; set; }
    }
 


}
