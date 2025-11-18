namespace Hooman.Application.DTO.Auth;

public record RoleModuleAccessDto(
  Guid Id,
  Guid RoleId,
  string RoleName,
  Guid ModuleId,
  string ModuleName,
  List<string> Permissions,
  DateTime CreatedAt,
  DateTime? UpdatedAt
);
