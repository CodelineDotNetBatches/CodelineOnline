using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a course within a category.
    /// </summary>
    /// <summary>
    /// Represents a course within a category.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Unique identifier for the course.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the course.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Foreign key to the parent category.
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Reference to the parent category.
        /// </summary>
        public Category Category { get; set; } = default!;

        /// <summary>
        /// Lessons in this course.
        /// </summary>
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

        /// <summary>
        /// Reviews submitted by users for this course.
        /// </summary>
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        /// <summary>
        /// Enrollments in this course.
        /// </summary>
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

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
