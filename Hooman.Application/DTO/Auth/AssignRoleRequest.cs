namespace Hooman.Application.DTO.Auth;

public record AssignRoleRequest(
  Guid UserId,
  Guid RoleId,
  DateTime? ExpiresAt = null
);
