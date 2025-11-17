using Hooman.Domain.Entities.Identity;

namespace Hooman.Domain.Entities.Assessment;

public class AssignmentSubmission
{
    public Guid Id { get; set; }
    public Guid AssignmentId { get; set; }
    public Guid StudentId { get; set; }
    public string? SubmissionText { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public bool IsLate { get; set; } = false;
    public decimal? Score { get; set; }
    public string? Feedback { get; set; }
    public string Status { get; set; } = "Submitted";
    public Guid? GradedBy { get; set; }
    public DateTime? GradedAt { get; set; }

    // Navigation Properties
    public virtual Assignment Assignment { get; set; } = null!;
    public virtual User Student { get; set; } = null!;
    public virtual ICollection<SubmissionFile> Files { get; set; } = new List<SubmissionFile>();
}
