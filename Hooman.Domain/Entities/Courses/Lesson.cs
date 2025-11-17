namespace Hooman.Domain.Entities.Courses;

public class Lesson
{
   public Guid Id { get; set; }
    public Guid SectionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty; // Video, Text, PDF, Quiz, Assignment
    public string? Content { get; set; }
    public string? VideoUrl { get; set; }
    public int? VideoDuration { get; set; }
    public string? DocumentUrl { get; set; }
    public int DisplayOrder { get; set; } = 0;
    public bool IsFree { get; set; } = false;
    public bool IsRequired { get; set; } = true;
    public int? EstimatedDuration { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; set; }

   public virtual CourseSection Section { get; set; } = null!;
    public virtual ICollection<LessonProgress> StudentProgress { get; set; } = new List<LessonProgress>();
}
