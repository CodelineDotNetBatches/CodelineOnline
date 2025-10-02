using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CoursesManagement.Models
{
    public class Enrollment
    {
        [Key]
        public Guid EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;


        // Foreign Keys and Navigation Properties ...
        public int TraineeId { get; set; }
        [ForeignKey(nameof(TraineeId))]
        //public Trainee Trainee { get; set; } = default!;
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = default!;
        public int? ProgramId { get; set; }
        [ForeignKey(nameof(ProgramId))]
        public Programs? Program { get; set; }


        //Additional note ...
        //modelBuilder.Entity<Enrollment>()
        //.HasIndex(e => new { e.StudentId, e.CourseId, e.ProgramId
        //})
        //.IsUnique();

    }
}
