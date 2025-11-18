namespace Hooman.Application.DTO.Auth;

public record UserModuleAccessDto(
  Guid Id, 
  Guid UserId,
  string UserName,
  string UserEmail,
  Guid ModuleId,
  string ModuleName,
  List<string> Permissions,
  string? GrantedBy,
  DateTime GrantedAt,
  DateTime? ExpiresAt,
  DateTime? UpdatedAt
);
