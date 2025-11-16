namespace Hooman.Domain.Entities.Identity;
public class RoleModule
{
    public Guid Id { get; set; }
    public Guid RoleId { get; set; }
    public Guid ModuleId { get; set; }
    public bool CanView { get; set; } = true;
    public bool CanCreate { get; set; } = false;
    public bool CanEdit { get; set; } = false;
    public bool CanDelete { get; set; } = false;
    public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    public Guid? GrantedBy { get; set; }

    // Navigation Properties
    public virtual Role Role { get; set; } = null!;
    public virtual Module Module { get; set; } = null!;
}
