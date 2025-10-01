using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace UserManagement.Models
{
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
        public string? ProfileImage { get; set; }
        
        [Required]
        [MaxLength(256)]
        public string InstructorCV { get; set; }
        
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
    }

    public enum ExperienceLevel 
    { 
        Junior = 1, 
        Mid = 2, 
        Senior = 3 
    }
    public enum TeachingStyle
    {
        ProjectBased = 1,
        TheoryFirst = 2,
        HandsOn = 3,
        Lecture = 4,
        Discussion = 5
    }

    public enum Specializations
    {
        CSharp = 1,
        Java = 2,
        Python = 3,
        JavaScript = 4,
        DataScience = 5,
        DevOps = 6,
        CloudComputing = 7,
        WebDevelopment = 8,
        MobileDevelopment = 9,
        CyberSecurity = 10
    }

}
