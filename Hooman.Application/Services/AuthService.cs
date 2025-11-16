using System.Security.Claims;
using System.Security.Cryptography;
using Hooman.Application.DTO.Auth;
using Hooman.Application.Interfaces;
using Hooman.Application.Settings;
using Hooman.Domain.Entities.Identity;
using Hooman.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Hooman.Application.Services;

public class AuthService : IAuthService
{
  private readonly ApplicationDbContext _context;
  private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly JwtSettings _jwtSettings;

     public AuthService(
        ApplicationDbContext context,
        IPasswordHasher passwordHasher,
        ITokenService tokenService,
        IEmailService emailService,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _emailService = emailService;
        _jwtSettings = jwtSettings.Value;
    }

  public async Task<AuthResponse> RegisterAsync(RegisterRequest request, string ipAddress)
  {
    // Validate
 if (await _context.Users.AnyAsync(u => u.Email == request.Email))
    throw new Exception("Email already registered");

  if (await _context.Users.AnyAsync(u => u.UserName == request.UserName))
    throw new Exception("Username already taken");

  if (request.Password != request.ConfirmPassword)
    throw new Exception("Passwords do not match");  
          
  // Hash Password
  var passwordHash = _passwordHasher.HashPassword(request.Password, out var salt);

  // Create User
  var user = new User
  {
    Id = Guid.NewGuid(),
    Email = request.Email,
    UserName = request.UserName,
    PasswordHash = passwordHash,
    PasswordSalt = salt,
    FirstName = request.FirstName,
    LastName = request.LastName,
    PhoneNumber = request.PhoneNumber,
    IsActive = true,
    IsEmailVerified = false,
    EmailVerificationToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
    CreatedAt = DateTime.UtcNow,
    UpdatedAt = DateTime.UtcNow
  };

  await _context.Users.AddAsync(user);

  // Assign Default student role
   var studentRole = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == "STUDENT");
        if (studentRole != null)
        {
            await _context.UserRoles.AddAsync(new UserRole
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                RoleId = studentRole.Id,
                AssignedAt = DateTime.UtcNow,
                IsActive = true
            });
        }


    await _context.SaveChangesAsync();


        // Generate tokens
    return await GenerateAuthResponse(user, ipAddress);
  }

  public async Task<AuthResponse> LoginAsync(LoginRequest request, string ipAddress)
    {
        // Find user by email or username
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.EmailOrUserName || 
                                     u.UserName == request.EmailOrUserName);

        if (user == null)
            throw new Exception("Invalid credentials");

        // Verify password
        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Invalid credentials");

        // Check if user is active
        if (!user.IsActive)
            throw new Exception("Account is deactivated");

        // Update last login
        user.LastLoginDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Generate tokens
        return await GenerateAuthResponse(user, ipAddress);
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
    {
        // Validate access token structure (don't check expiry)
        var principal = _tokenService.ValidateToken(request.AccessToken);
        if (principal == null)
            throw new Exception("Invalid token");

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdClaim, out var userId))
            throw new Exception("Invalid token");

        // Find user
        var user = await _context.Users.FindAsync(userId);
        if (user == null || !user.IsActive)
            throw new Exception("Invalid token");

        // Find refresh token
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken && rt.UserId == userId);

        if (refreshToken == null || !refreshToken.IsActive)
            throw new Exception("Invalid refresh token");

        // Validate JWT ID matches
        var jwtId = _tokenService.GetJwtIdFromToken(request.AccessToken);
        if (refreshToken.JwtId != jwtId)
            throw new Exception("Token mismatch");

        // Mark old token as used
        refreshToken.IsUsed = true;
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedById = ipAddress;

        // Generate new tokens
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        refreshToken.ReplacedByToken = newRefreshToken;

        await _context.SaveChangesAsync();

        // Generate new auth response
        return await GenerateAuthResponse(user, ipAddress);
    }

    public async Task<bool> RevokeTokenAsync(string token, string ipAddress)
    {
        var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null || !refreshToken.IsActive)
            return false;

        refreshToken.IsRevoked = true;
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedById = ipAddress;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
            throw new Exception("User not found");

        // Verify current password
        if (!_passwordHasher.VerifyPassword(request.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            throw new Exception("Current password is incorrect");

        if (request.NewPassword != request.ConfirmNewPassword)
            throw new Exception("Passwords do not match");

        // Hash new password
        user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword, out var salt);
        user.PasswordSalt = salt;
        user.UpdatedAt = DateTime.UtcNow;

        // Revoke all refresh tokens for security
        var refreshTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId && rt.IsActive)
            .ToListAsync();

        foreach (var rt in refreshTokens)
        {
            rt.IsRevoked = true;
            rt.RevokedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
            return true; // Don't reveal if email exists

        user.PasswordResetToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        user.PasswordResetTokenExpiry = DateTime.UtcNow.AddHours(24);

        await _context.SaveChangesAsync();

        // TODO: Send email with reset link
        // Email service would send: /reset-password?email={email}&token={token}

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && 
                                     u.PasswordResetToken == request.Token);

        if (user == null || user.PasswordResetTokenExpiry < DateTime.UtcNow)
            throw new Exception("Invalid or expired reset token");

        if (request.NewPassword != request.ConfirmNewPassword)
            throw new Exception("Passwords do not match");

        // Reset password
        user.PasswordHash = _passwordHasher.HashPassword(request.NewPassword, out var salt);
        user.PasswordSalt = salt;
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> VerifyEmailAsync(string email, string token)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email && u.EmailVerificationToken == token);

        if (user == null)
            throw new Exception("Invalid verification token");

        user.IsEmailVerified = true;
        user.EmailVerificationToken = null;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }


  private async Task<AuthResponse> GenerateAuthResponse(User user, string ipAddress)
  {
    // Get user roles and permissions
    var userRoles = await _context.UserRoles
        .Include(ur => ur.Role)
          .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
        .Where(ur => ur.UserId == user.Id && ur.IsActive)
        .ToListAsync();
    
    var roles = userRoles.Select(ur => ur.Role.Name).ToList();
    var permissions = userRoles
        .SelectMany(ur => ur.Role.RolePermissions)
        .Select(rp => rp.Permission.Name)
        .Distinct()
        .ToList();

    // Generate tokens
    var accessToken = _tokenService.GenerateAccessToken(user, roles, permissions);
    var refreshToken = _tokenService.GenerateRefreshToken();
    var jwtId = _tokenService.GetJwtIdFromToken(accessToken);
  
  
            await _context.RefreshTokens.AddAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            JwtId = jwtId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            CreatedById = ipAddress
        });

        await _context.SaveChangesAsync();

        // Build response
        return new AuthResponse(
            accessToken,
            refreshToken,
            DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            new UserDto(
                user.Id,
                user.Email,
                user.UserName,
                user.FirstName,
                user.LastName,
                user.FullName,
                user.PhoneNumber,
                user.ProfileImageUrl,
                user.IsActive,
                user.IsEmailVerified,
                user.LastLoginDate,
                userRoles.Select(ur => new RoleDto(
                    ur.Role.Id,
                    ur.Role.Name,
                    ur.Role.Description ?? string.Empty,
                    ur.Role.IsSystemRole
                )).ToList(),
                permissions
            )
        );

  }
}
