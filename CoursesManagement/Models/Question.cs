namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a quiz question.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Unique identifier for the question.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Text of the question.
        /// </summary>
        public string Text { get; set; } = default!;

        /// <summary>
        /// Foreign key to the parent quiz.
        /// </summary>
        public Guid QuizId { get; set; }

        /// <summary>
        /// Reference to the parent quiz.
        /// </summary>
        public Quiz Quiz { get; set; } = default!;

        /// <summary>
        /// Answers submitted by users to this question.
        /// </summary>
        public ICollection<UserAnswer> Answers { get; set; } = new List<UserAnswer>();
    }

}
