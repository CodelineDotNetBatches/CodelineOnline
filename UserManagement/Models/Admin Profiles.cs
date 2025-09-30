using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    // Represents the Admin Profile entity (table in the database)
    public class Admin_Profiles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Manual entry for AdminId

        public int AdminId { get; set; }

        // Navigation property for related responsibilities
        // One Admin can have many Responsibilities
        public ICollection<Responsibility> Responsibilitys {  get; set; }


    }
}
