public record UpdateModuleRequest(
  string Name,
  string Code,
  string? Description,
  string? Icon,
  string? Route,
  Guid? CategoryId,
  Guid? ParentModuleId,
  int DisplayOrder,
  bool RequiresAuth,
  List<string> AvailablePermissions
);
