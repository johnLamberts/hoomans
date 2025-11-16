namespace Hooman.Application.DTO.Auth;
public record UserDto(
    Guid Id,
    string Email,
    string Username,
    string FirstName,
    string LastName,
    string FullName,
    string? PhoneNumber,
    string? ProfileImageUrl,
    bool IsActive,
    bool IsEmailVerified,
    DateTime? LastLoginDate,
    List<RoleDto> Roles,
    List<string> Permissions
);
