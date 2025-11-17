namespace Hooman.Domain.Entities.Assessment;

public class Question
{
    public Guid Id { get; set; }
    public Guid? QuestionBankId { get; set; }
    public string QuestionType { get; set; } = string.Empty;
    public string QuestionText { get; set; } = string.Empty;
    public string? QuestionHtml { get; set; }
    public string? QuestionJson { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Points { get; set; } = 1;
    public string? DifficultyLevel { get; set; }
    public string? ExplanationText { get; set; }
    public string? Tags { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public virtual QuestionBank? QuestionBank { get; set; }
    public virtual ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    public virtual ICollection<AssessmentQuestion> AssessmentQuestions { get; set; } = new List<AssessmentQuestion>();
}
