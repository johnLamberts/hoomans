using Hooman.Domain.Entities.Identity;

namespace Hooman.Domain.Entities.Courses;

public class Course
{
  public Guid Id {get;set;}
  public string Title {get;set;} = string.Empty;
  public string Slug {get;set;} = string.Empty;
  public string? ShortDescription {get;set;}
  public string? FullDescription {get;set;}

  public Guid? CategoryId {get;set;}
  public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
  public string Language {get;set;} = "en";
  public string? ThumbnailUrl {get;set;}
  public string? TrailerVideoUrl {get;set;}
  public int? EstimatedDuration {get;set;}
  public decimal Price {get;set;} = 0;
  public bool IsFree {get;set;} = false;
  public bool IsPublished {get;set;} = false;
  public DateTime? PublishedAt {get;set;}
  public bool IsFeatured {get;set;}
  public bool RequiresEnrollment {get;set;} = true;
  public int? MaxStudents {get;set;}
  public bool CertificateEnabled {get;set;}  = false;

  public decimal? PassingScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
 public virtual Category? Category { get; set; }
    public virtual User Creator { get; set; } = null!;
    public virtual ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
    public virtual ICollection<CourseSection> Sections { get; set; } = new List<CourseSection>();
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<CourseTag> Tags { get; set; } = new List<CourseTag>();
    public virtual ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
    public virtual ICollection<CourseLearningObjective> LearningObjectives { get; set; } = new List<CourseLearningObjective>();
    public virtual ICollection<CourseRequirement> Requirements { get; set; } = new List<CourseRequirement>();

}
