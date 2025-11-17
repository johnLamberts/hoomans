namespace Hooman.Domain.Entities.Courses;

public class CourseRequirement
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Requirement { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
}
