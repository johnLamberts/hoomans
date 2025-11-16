namespace Hooman.Application.DTO.Auth;
public record RoleDto(
    Guid Id,
    string Name,
    string Description,
    bool IsSystemRole,
    List<PermissionDto>? Permissions = null
);
