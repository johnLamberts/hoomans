namespace Hooman.Domain.Entities.Identity;

public class ModulePermission
{
    public Guid Id { get; set; }
    
    // The permission name (e.g., "Read", "Write", or a full key "User.Create")
    public string Name { get; set; } = string.Empty;
    
    public string DisplayName { get; set; } = string.Empty;
    
    // Foreign Key to the owning Module
    public Guid ModuleId { get; set; }

    // Navigation Property
    public virtual Module Module { get; set; } = null!;
}
