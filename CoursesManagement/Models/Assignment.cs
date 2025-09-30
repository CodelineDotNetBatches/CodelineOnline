using CoursesManagement.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents an assignment given in a course.
/// </summary>
public class Assignment
{
    /// <summary>
    /// Unique identifier for the assignment.
    /// </summary>
    [Key]
    public int AssignmentId { get; set; }

    /// <summary>
    /// Foreign key to the parent course.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    /// Navigation property to the parent course.
    /// </summary>
    [ForeignKey(nameof(CourseId))]
    public Course Course { get; set; } = default!;

    /// <summary>
    /// Title of the course (redundant but stored for quick lookup/reporting).
    /// </summary>
    [Required, MaxLength(200)]
    public string CourseTitle { get; set; } = default!;

    /// <summary>
    /// The due date for the assignment.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// The assignment question or task.
    /// </summary>
    [Required]
    public string Question { get; set; } = default!;

    /// <summary>
    /// The expected/model answer for the assignment.
    /// </summary>
    public string? Answer { get; set; }

    /// <summary>
    /// Maximum mark/points for this assignment.
    /// </summary>
    public int Mark { get; set; }
}
