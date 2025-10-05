

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class Skill
    {
        [Key]
        public int SkillId { get; set; }
        [Required]
        public string SkillName { get; set; }
        public string? SkillLevel { get; set; }
        public int? MonthsOfExperience { get; set; }
        // Foreign Key reference to the user
        [ForeignKey("Trainee")]
        public int UserId { get; set; }
        // Navigation property
        public Trainee? Trainee { get; set; }
    }

}
