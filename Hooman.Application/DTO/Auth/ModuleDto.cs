using Hooman.Domain.Entities.Identity;

namespace Hooman.Application.DTO.Auth;

public record ModuleDto(
  Guid Id,
  string Name,
  string Code,
  string? Description,
  string? Icon,
  string? Route,
  
  // NEW: Category property
  ModuleCategoryDto? Category, 

  Guid? ParentModuleId,
  string? ParentModuleName,
  int DisplayOrder,
  bool IsActive,
  bool RequiresAuth,
  
  // FIXED: Type must match the type being passed from the entity
  ICollection<ModulePermission> AvailablePermissions, // Changed from List<string> to ICollection<ModulePermission>

  List<string>? GrantedPermissions,
  string? AccessSource,
  DateTime? ExpiresAt,
  List<ModuleDto> ChildModules
);
