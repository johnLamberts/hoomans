namespace Hooman.Domain.Entities.Courses;

public class CourseSection
{
  public Guid Id {get;set;}
  public Guid CourseId {get;set;}
  public string Title {get;set;} = string.Empty;
  public string? Description {get;set;}
  public int DisplayOrder {get;set;} = 0;
  public bool IsActive {get;set;} = true;
  
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

}
