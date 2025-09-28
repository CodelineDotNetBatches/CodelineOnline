namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a lesson within a course.
    /// </summary>
    public class Lesson
    {
        /// <summary>
        /// Unique identifier for the lesson.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the lesson.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Main content of the lesson.
        /// </summary>
        public string Content { get; set; } = default!;

        /// <summary>
        /// Foreign key to the parent course.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Reference to the parent course.
        /// </summary>
        public Course Course { get; set; } = default!;

        /// <summary>
        /// Quizzes belonging to this lesson.
        /// </summary>
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }

}
