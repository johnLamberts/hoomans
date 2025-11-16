namespace Hooman.Domain.Entities.Identity;
public class Permission
{
  public Guid Id {get;set;}
  public string Name {get;set;} = string.Empty;
  public string NormalizedName {get;set;} = string.Empty;
  public string Category {get;set;} = string.Empty;
  public string? Description {get;set;}
  public bool IsSystemPermission {get;set;} =false;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();


}
