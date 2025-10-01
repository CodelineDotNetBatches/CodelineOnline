using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a lesson within a course.
    /// </summary>
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        [Required]
        public string Title { get; set; } = default!;

        [Required]
        public string? ContentText { get; set; }
        [Required]
        public string? ContentType { get; set; }
        [Required]
        public string? ResourcesUrl { get; set; }

        // Foreign Keys
        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; } = default!;

    }

}
