

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class TraineeSkill
    {
        [Required]
        public int TraineeSkillId { get; set; }
        [Required]
        public string SkillName { get; set; }
        public string? SkillLevel { get; set; }
        public int? MonthsOfExperience { get; set; }
        // Foreign Key reference to the user

        [ForeignKey("Trainee")]
        public int TraineeId { get; set; }



        // Navigation property
        public Trainee Trainee { get; set; } // one skill belongs to one trainee
    }

}
