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

        // Navigation property for related responsibilities
        // One Admin can have many Responsibilities
        public ICollection<Responsibility> Responsibilitys {  get; set; }

    }

    // Represents the Responsibility entity (table in the database)
    public class Responsibility
    {
        [ForeignKey("Admin_Profile")] // Declares AdminId as a Foreign Key linked to AdminProfile
        public int AdminId { get; set; } // References the Admin who owns this Responsibility


    }
}
