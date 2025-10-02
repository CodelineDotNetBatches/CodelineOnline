using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents an answer submitted by a user to a specific question.
/// </summary>
public class UserAnswer
{
    /// <summary>
    /// Unique identifier for the user answer.
    /// </summary>
    [Key]
    public int UserAnswerId { get; set; }

    /// <summary>
    /// The actual response text provided by the user.
    /// </summary>
    [Required]
    public string Response { get; set; } = default!;

    /// <summary>
    /// Indicates whether the submitted answer is correct.
    /// </summary>
    public bool IsCorrect { get; set; }

    /// <summary>
    /// The date and time when the answer was submitted.
    /// </summary>
    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Foreign key to the related question.
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Navigation property to the related question.
    /// </summary>
    [ForeignKey(nameof(QuestionId))]
    public Question Question { get; set; } = default!;

    /// <summary>
    /// Optional foreign key to the user (demo user only, not persisted).
    /// </summary>
    public Guid? UserId { get; set; }
}
