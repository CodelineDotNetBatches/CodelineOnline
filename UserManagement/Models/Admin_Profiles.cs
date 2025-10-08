using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    // Represents the Admin Profile entity (table in the database)
    public class Admin_Profile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Manual entry for AdminId

        public int AdminId { get; set; }

        [ForeignKey("Branch")]
        public int  BranchId { get; set; }

        // Navigation property for related responsibilities
        // One Admin can have many Responsibilities
        public virtual ICollection<Responsibility> Responsibilitys {  get; set; }
        public virtual ICollection<Batch> Batchs { get; set; } // one admin can manage many batch 

        public virtual Branch branchs { get; set; }



    }

    // Represents the Responsibility entity (table in the database)
    public class Responsibility
    {
        [ForeignKey("Admin_Profile")] // Declares AdminId as a Foreign Key linked to AdminProfile
        public int AdminId { get; set; } // References the Admin who owns this Responsibility

        [StringLength(500, ErrorMessage = "Responsibility details cannot exceed 500 characters")]
        public string ResponsibilityTitle { get; set; } // Extra details or notes about the responsibility
        public string ResponsibilityDetails { get; set; } // Extra details or notes about the responsibility

        // Navigation 
        public virtual Admin_Profile AdminProfile { get; set; }



    }
}
