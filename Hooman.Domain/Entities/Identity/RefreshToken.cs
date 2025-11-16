namespace Hooman.Domain.Entities.Identity;

public class RefreshToken
{
  public Guid Id {get;set;}
  public Guid UserId {get;set;}
  public string Token {get;set;} = string.Empty;
  public string JwtId {get;set;} = string.Empty;
  public bool IsUsed {get;set;} = false;
  public bool IsRevoked {get;set;} = false;
  public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
  public DateTime ExpiresAt {get;set;} = DateTime.UtcNow;
  public DateTime? RevokedAt {get;set;} = DateTime.UtcNow;

  public string? RevokedById {get;set;}
  public string? ReplacedByToken {get;set;}
  public string CreatedById {get;set;} = string.Empty;

  // Navigation Properties
  public virtual User User {get;set;} = null!;
  public bool IsActive => !IsUsed && !IsRevoked && ExpiresAt > DateTime.UtcNow;
}
