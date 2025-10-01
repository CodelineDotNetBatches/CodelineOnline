using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a quiz within a lesson.
    /// </summary>
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public int TotalMark { get; set; }

        // Foreign Keys
        public int LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        public Lesson Lesson { get; set; } = default!;

        // Navigation
        public ICollection<Question> Questions { get; set; } = new List<Question>();


    }
}
