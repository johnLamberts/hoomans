namespace Hooman.Domain.Entities.Assessment;


public class QuestionOption
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
