using Hooman.Domain.Entities.Identity;

namespace Hooman.Domain.Entities.Courses;

public class CourseReview
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public int Rating { get; set; }
    public string? ReviewText { get; set; }
    public bool IsApproved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
}
