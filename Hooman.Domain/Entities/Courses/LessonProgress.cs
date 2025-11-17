namespace Hooman.Domain.Entities.Courses;

public class LessonProgress
{
   public Guid Id { get; set; }
    public Guid EnrollmentId { get; set; }
    public Guid LessonId { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime? CompletedAt { get; set; }
    public int? TimeSpent { get; set; }
    public DateTime? LastAccessedAt { get; set; }
    public string? Notes { get; set; }

    // Navigation Properties
    public virtual Enrollment Enrollment { get; set; } = null!;
    public virtual Lesson Lesson { get; set; } = null!;
}
