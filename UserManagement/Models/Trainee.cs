using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Models
{
    public enum LearningStyle
    {
        ProjectBased,
        SelfPaced,
        GroupStudy,
        Lecture,
        Interactive
    }

    public enum StudyFocus
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

    public enum ExperienceLevel
    {
        Junior,
        Mid,
        Senior,
        Lead
    }

    public class Trainee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TraineeId { get; set; }

        // link with batch entity 
        [ForeignKey("Batch")]
        public int BatchId { get; set; }

        [MaxLength(50)]
        public string GithubUsername { get; set; } = string.Empty;

        [Required]
        [Range(0, 60)]
        public int Years_of_Experience { get; set; }

        [MaxLength(256)]
        public string? ProfileImage { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string TraineeCV { get; set; } = string.Empty;

        [Required]
        public ExperienceLevel Experience_Level { get; set; }

        [Required]
        public LearningStyle Learning_Style { get; set; }

        [Required]
        public StudyFocus Study_Focus { get; set; }

        [MaxLength(256)]
        public string? EducationalBackground { get; set; }

        [MaxLength(256)]
        public string? LearningObjectives { get; set; }

        // Navigation

        public ICollection<TraineeSkill> traineeSkills { get; set; } = new List<TraineeSkill>(); // one trainee can has many skills 
        public Batch Batch { get; set; } // one trainee can join with onw batch 

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>(); // one trainee can has many instructor (mentor)

    }
}
