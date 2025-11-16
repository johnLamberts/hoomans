namespace Hooman.Domain.Entities.Identity;
public class Module
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? RoutePrefix { get; set; }
    public bool IsSystemModule { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();
    public virtual ICollection<UserModule> UserModules { get; set; } = new List<UserModule>();
}
