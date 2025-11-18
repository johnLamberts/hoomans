using Hooman.Domain.Entities.Courses;

namespace Hooman.Domain.Entities.Identity;

public class Module
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NormalizedName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;

    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public string? Route { get; set; }

    // Hierarchy Properties
    public Guid? ParentModuleId { get; set; } // Foreign Key to Parent Module
    public Guid? CategoryId { get; set; }

    // Core Module Flags
    public bool RequiresAuth { get; set; } = true; // Added from DTO implication
    public bool IsSystemModule { get; set; } = false;
    public bool IsActive { get; set; } = true;

    // Auditing/Ordering
    public int DisplayOrder { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    
    // Hierarchy
    public virtual Module? ParentModule { get; set; } // Reference to the Parent
    public virtual ICollection<Module> ChildModules { get; set; } = new List<Module>(); // Children of this Module
    public virtual Category Category {get;set;} = null!;
    // Relationships
    public virtual ICollection<ModulePermission> AvailablePermissions { get; set; } = new List<ModulePermission>(); 
    public virtual ICollection<RoleModule> RoleModules { get; set; } = new List<RoleModule>();
    public virtual ICollection<UserModule> UserModules { get; set; } = new List<UserModule>();
}
