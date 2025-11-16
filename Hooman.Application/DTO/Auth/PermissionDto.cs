namespace Hooman.Application.DTO.Auth;
public record PermissionDto(
    Guid Id,
    string Name,
    string Category,
    string Description
);
