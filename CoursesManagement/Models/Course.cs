using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    public enum LevelType
    {
        Beginner,
        Intermediate,
        Advanced
    }
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        [Required, MaxLength(200)]
        public string CourseName { get; set; } = default!;

        [MaxLength(1000)]
        public string? CourseDescription { get; set; }
        [Required]
        public LevelType CourseLevel { get; set; }

        //public int Duration { get; set; }
        //public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Keys and Navigation Properties ...

        //every course belongs to one category ... one to many relationship
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = default!;

        //every course is belongs to many programs ... many to many relationship ...
        //public ICollection<Programs> Programs { get; set; } = new List<Programs>();
        //every course has many enrollments ... many to many relationship ...
        //public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        //every course has many notification ... one to many relationship ...
        //public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        //every course has many UserProgress ... many to many relationship ...
        //public ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();
        //every course has many lessons ... one to many relationship ...
        //public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        //every course has many assignments ... one to many relationship ...
        //public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
        //every course has many reviews ... one to many relationship ...
        //public ICollection<Review> Reviews { get; set; } = new List<Review>();
        //every course has many quizzes ... one to many relationship ...
        //public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

        // Instructor of the course ... many to one relationship ...
        //public int InstructorId { get; set; }
        //[ForeignKey(nameof(InstructorId))]
        //public Instructor Instructor { get; set; } = default!;

        /// <summary>
        /// Assignments belonging to this course.
        /// </summary>
        public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

        /// <summary>
        /// Certificates issued after course completion.
        /// </summary>
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
    }


}
