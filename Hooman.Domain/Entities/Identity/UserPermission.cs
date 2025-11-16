namespace Hooman.Domain.Entities.Identity;

public class UserPermission
{
  public Guid Id {get;set;}
  public Guid UserId {get;set;}
  public Guid PermissionId {get;set;}
  public bool IsGranted {get;set;}
  public DateTime GrantedAT {get;set;} = DateTime.UtcNow;
  public Guid? GrantedBy {get;set;}
  public DateTime? ExpiresAt {get;set;}

  // Navigation Properties
  public virtual User User {get;set;} = null!;
  public virtual Permission Permission {get;set;} = null!;

}
