namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a learning program that groups categories and courses.
    /// </summary>
    /// <summary>
    /// Represents a learning program that groups categories and courses.
    /// </summary>
    /// <summary>
    /// Represents a learning program that groups categories and courses.
    /// </summary>
    public class Programs
    {
        /// <summary>
        /// Unique identifier for the program.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the program.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// Description of the program.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Categories under this program.
        /// </summary>
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }



}
