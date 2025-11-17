using Hooman.Domain.Entities.Identity;

namespace Hooman.Domain.Entities.Courses;

public class CourseInstructor
{
  public Guid Id {get;set;}
  public Guid CourseId {get;set;}
  public Guid InstructorId {get;set;}
  public bool IsPrimary {get;set;} = false;
  public DateTime AssignedAt {get;set;} = DateTime.UtcNow;

  // Navigation poperties
  public virtual Course Course {get;set;} = null!;
  public virtual User Instructor {get;set;} = null!;
}
