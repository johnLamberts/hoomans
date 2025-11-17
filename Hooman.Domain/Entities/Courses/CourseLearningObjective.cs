namespace Hooman.Domain.Entities.Courses;

public class CourseLearningObjective
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Objective { get; set; } = string.Empty;
    public int DisplayOrder { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
}
