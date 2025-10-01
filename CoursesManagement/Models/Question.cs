using CoursesManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Represents a question within a quiz.
/// </summary>
public class Question
{
    /// <summary>
    /// Unique identifier for the question.
    /// </summary>
    [Key]
    public int QuestionId { get; set; }

    /// <summary>
    /// The text of the question.
    /// </summary>
    [Required]
    public string Text { get; set; } = default!;

    /// <summary>
    /// The type of the question (MCQ, True/False, ShortAnswer, etc.).
    /// </summary>
    [Required]
    public string Type { get; set; } = "MultipleChoice";

    /// <summary>
    /// The correct answer to the question.
    /// </summary>
    public string? CorrectAnswer { get; set; }

    /// <summary>
    /// Foreign key to the parent quiz.
    /// </summary>
    public int QuizId { get; set; }

    /// <summary>
    /// Navigation property to the parent quiz.
    /// </summary>
    [ForeignKey(nameof(QuizId))]
    public Quiz Quiz { get; set; } = default!;

    /// <summary>
    /// Collection of user answers for this question.
    /// </summary>
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
