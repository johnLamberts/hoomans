namespace Hooman.Application.DTO.Auth;

public record ModuleCategoryDto(
  Guid Id,
  string Name,
  string? Description,
  int DisplayOrder, 
  int ModuleCount
);
