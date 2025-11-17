namespace Hooman.Domain.Entities.Assessment;

public class RubricCriteria
{
    public Guid Id { get; set; }
    public Guid RubricId { get; set; }
    public string CriteriaName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal MaxPoints { get; set; }
    public int DisplayOrder { get; set; } = 0;

    // Navigation Properties
    public virtual Rubric Rubric { get; set; } = null!;
    public virtual ICollection<RubricLevel> Levels { get; set; } = new List<RubricLevel>();
}
