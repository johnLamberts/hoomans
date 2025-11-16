namespace Hooman.Application.DTO.Auth;
public record GrantPermissionRequest(
    Guid UserId,
    Guid PermissionId,
    bool IsGranted = true,
    DateTime? ExpiresAt = null
);
