namespace Hooman.Application.DTO.Auth;
public record UserRoleDto(
    Guid Id,
    Guid UserId,
    RoleDto Role,
    DateTime AssignedAt,
    DateTime? ExpiresAt,
    bool IsActive
);
