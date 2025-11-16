namespace Hooman.Domain.Entities.Identity;

public class User
{
  public Guid Id {  get;  set; }
  public string Email {get;set;} = string.Empty;
  public string UserName {get;set;} = string.Empty;
  public string PasswordHash {get;set;} = string.Empty;
  public string PasswordSalt {get;set;} = string.Empty;
  public string FirstName {get;set;} = string.Empty;
  public string LastName {get;set;} = string.Empty;

  public string? PhoneNumber {get;set;}
  public string? ProfileImageUrl {get;set;}
  public bool IsActive {get;set;} = true;
  public bool IsEmailVerified {get;set;} = false;
    public string? EmailVerificationToken { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? PasswordResetTokenExpiry { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid? CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }

    // Navigation properties
      public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public virtual ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    

    public string FullName => $"{FirstName} {LastName}";
}
