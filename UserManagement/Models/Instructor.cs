using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace UserManagement.Models
{
    public enum Experience_Level
    {
        Junior,
        Mid,
        Senior,
        Lead
    }
    public enum TeachingStyle
    {
        ProjectBased,
        TheoryFirst,
        HandsOn,
        Lecture,
        Discussion
    }

    public enum Specializations
    {
        CSharp,
        Java,
        Python,
        JavaScript,
        DataScience,
        DevOps,
        CloudComputing,
        WebDevelopment,
        MobileDevelopment,
        CyberSecurity
    }
    public class Instructor
    {
        // PK=FK to Users.UserId (NO IDENTITY)
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Important: no identity, value provided by app (UserId)

        public int InstructorId { get; set; }

        [MaxLength(50)]
        public string GithubUserName { get; set; } = string.Empty;

        [Required]
        [Range(0, 60)]
        public int Years_of_Experience { get; set; }

        [MaxLength(256)]
        public string? ProfileImage { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string InstructorCV { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public ExperienceLevel Experience_Level { get; set; } // e.g., Junior/Mid/Senior

        [Required]
        [MaxLength(50)]
        public TeachingStyle Teaching_Style { get; set; } // e.g., Project-based, Theory-first


        // Added to satisfy “filter by expertise/specialization” requirement
        [Required]
        [MaxLength(80)]
        public Specializations Specialization { get; set; } // e.g., C#, Data Science, DevOps

        // navigation property
        public ICollection<Availability> Availabilities { get; set; }

        public ICollection<InstructorSkill> instructorSkills { get; set; } = new List<InstructorSkill>(); // one instructor can has many skills
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>(); // one instructor can mentor many trainee

        public ICollection<Batch> Batches { get; set; } = new List<Batch>(); // one instructor can teach many batch

        

    }



}
