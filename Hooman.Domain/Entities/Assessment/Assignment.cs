using Hooman.Domain.Entities.Courses;

namespace Hooman.Domain.Entities.Assessment;

public class Assignment
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid CourseId { get; set; }
    public string? Instructions { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal MaxScore { get; set; } = 100;
    public bool AllowLateSubmission { get; set; } = true;
    public decimal? LatePenaltyPercentage { get; set; }
    public string? AllowedFileTypes { get; set; }
    public int? MaxFileSize { get; set; }
    public bool RequireFileUpload { get; set; } = false;
    public bool RequireTextSubmission { get; set; } = false;
    public bool IsPublished { get; set; } = false;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<AssignmentSubmission> Submissions { get; set; } = new List<AssignmentSubmission>();
}
