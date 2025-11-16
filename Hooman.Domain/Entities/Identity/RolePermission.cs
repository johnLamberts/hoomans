namespace Hooman.Domain.Entities.Identity;

public class RolePermission
{
  public Guid Id {get;set;}
  public Guid RoleId {get;set;}
  public Guid PermissionId {get;set;}
  public DateTime GrantedAt {get;set;} = DateTime.UtcNow;
  public Guid? GrantedBy {get;set;}

  // Navigation Properties
  public virtual Role Role {get;set;} = null!;
  public virtual Permission Permission {get;set;} = null!;
}
