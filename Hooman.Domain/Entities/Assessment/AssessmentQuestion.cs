namespace Hooman.Domain.Entities.Assessment;

public class AssessmentQuestion
{
    public Guid Id { get; set; }
    public Guid AssessmentId { get; set; }
    public Guid QuestionId { get; set; }
    public int DisplayOrder { get; set; } = 0;
    public decimal? Points { get; set; }
    public bool IsRequired { get; set; } = true;

    // Navigation Properties
    public virtual Assessment Assessment { get; set; } = null!;
    public virtual Question Question { get; set; } = null!;
}
