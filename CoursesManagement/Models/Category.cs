using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesManagement.Models
{
    /// <summary>
    /// Represents a category that groups related courses under programs.
    /// </summary>
    public class Category
    {
        // ======================
        // Primary Key
        // ======================

        [Key]
        public Guid CategoryId { get; set; }

        // ======================
        // Required Fields
        // ======================

        [Required, MaxLength(200)]
        public string CategoryName { get; set; } = null!;

        [MaxLength(500)]
        public string? CategoryDescription { get; set; }

        // ======================
        // Timestamps
        // ======================

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // ======================
        // Relationships
        // ======================

        /// <summary>
        /// Navigation property: many-to-many relationship with Programs.
        /// </summary>
        public virtual ICollection<Programs> Programs { get; set; } = new List<Programs>();

        /// <summary>
        /// Navigation property: one-to-many relationship with Courses.
        /// </summary>
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
