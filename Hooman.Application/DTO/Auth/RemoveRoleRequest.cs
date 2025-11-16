namespace Hooman.Application.DTO.Auth;
public record RemoveRoleRequest(
    Guid UserId,
    Guid RoleId
);
