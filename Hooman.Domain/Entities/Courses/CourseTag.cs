namespace Hooman.Domain.Entities.Courses;

public class CourseTag
{
   public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Tag { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
}
