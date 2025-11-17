namespace Hooman.Domain.Entities.Assessment;

public class AssessmentAnswer
{
    public Guid Id { get; set; }
    public Guid AttemptId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid? SelectedOptionId { get; set; }
    public string? AnswerText { get; set; }
    public string? AnswerJson { get; set; }
    public bool? IsCorrect { get; set; }
    public decimal? PointsEarned { get; set; }
    public decimal? PointsPossible { get; set; }
    public string? Feedback { get; set; }
    public int? TimeSpent { get; set; }
    public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual AssessmentAttempt Attempt { get; set; } = null!;
    public virtual Question Question { get; set; } = null!;
    public virtual QuestionOption? SelectedOption { get; set; }
}
