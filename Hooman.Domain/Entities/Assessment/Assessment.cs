namespace Hooman.Domain.Entities.Assessment;

public class Assessment
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string AssessmentType { get; set; } = string.Empty; // Quiz, Exam, Assignment, Survey
    public Guid? CourseId { get; set; }
    public Guid? LessonId { get; set; }
    public string? Instructions { get; set; }
    public int? TimeLimit { get; set; }
    public decimal? PassingScore { get; set; }
    public int? MaxAttempts { get; set; }
    public bool ShuffleQuestions { get; set; } = false;
    public bool ShuffleOptions { get; set; } = false;
    public bool ShowFeedback { get; set; } = true;
    public bool ShowCorrectAnswers { get; set; } = true;
    public DateTime? ShowCorrectAnswersAfter { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public bool IsPublished { get; set; } = false;
    public bool RequiresProctor { get; set; } = false;
    public bool AllowReview { get; set; } = true;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course? Course { get; set; }
    public virtual Lesson? Lesson { get; set; }
    public virtual ICollection<AssessmentQuestion> Questions { get; set; } = new List<AssessmentQuestion>();
    public virtual ICollection<AssessmentAttempt> Attempts { get; set; } = new List<AssessmentAttempt>();
}
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public string OptionText { get; set; } = string.Empty;
    public string? OptionHtml { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsCorrect { get; set; } = false;
    public int DisplayOrder { get; set; } = 0;
    public string? Feedback { get; set; }

    // Navigation Properties
    public virtual Question Question { get; set; } = null!;
}
