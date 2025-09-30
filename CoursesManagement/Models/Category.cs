namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a course category under a program.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Foreign key to the parent program.
        /// </summary>
        public Guid ProgramsId { get; set; }

        /// <summary>
        /// Reference to the parent program.
        /// </summary>
        public Programs Programs { get; set; } = default!;

        /// <summary>
        /// Courses within this category.
        /// </summary>
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }

}
