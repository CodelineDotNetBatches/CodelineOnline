

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class InstructorSkill
    {
        ////[Key]
        [Required]
        public int InstructorSkillId { get; set; }
        [Required]
        public string SkillName { get; set; }
        public string? SkillLevel { get; set; }
        public int? MonthsOfExperience { get; set; }
        // Foreign Key reference to the user
        
        [ForeignKey("Instructor")]
        public int InstructorId { get; set; }

       

        // Navigation property
        public virtual Instructor Instructor { get; set; } // one skill belongs to one instructor
    }

}
