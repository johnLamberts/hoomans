namespace Hooman.Domain.Entities.Assessment;


public class RubricLevel
{
    public Guid Id { get; set; }
    public Guid CriteriaId { get; set; }
    public string LevelName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Points { get; set; }
    public int DisplayOrder { get; set; } = 0;

    // Navigation Properties
    public virtual RubricCriteria Criteria { get; set; } = null!;
}
