namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a quiz within a lesson.
    /// </summary>
    public class Quiz
    {
        /// <summary>
        /// Unique identifier for the quiz.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the quiz.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Foreign key to the parent lesson.
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Reference to the parent lesson.
        /// </summary>
        public Lesson Lesson { get; set; } = default!;

        /// <summary>
        /// Questions belonging to this quiz.
        /// </summary>
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
