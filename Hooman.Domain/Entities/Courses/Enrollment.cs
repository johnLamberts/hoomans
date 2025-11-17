using Hooman.Domain.Entities.Identity;

namespace Hooman.Domain.Entities.Courses;

public class Enrollment
{
  public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid StudentId { get; set; }
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    public DateTime? CompletionDate { get; set; }
    public string Status { get; set; } = "Active"; // Active, Completed, Dropped, Suspended
    public decimal Progress { get; set; } = 0;
    public DateTime? LastAccessedAt { get; set; }
    public DateTime? CertificateIssuedAt { get; set; }
    public string? CertificateUrl { get; set; }
    public Guid? EnrolledBy { get; set; }
    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
    public virtual ICollection<LessonProgress> LessonProgress { get; set; } = new List<LessonProgress>();
}
