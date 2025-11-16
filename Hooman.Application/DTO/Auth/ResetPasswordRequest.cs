namespace Hooman.Application.DTO.Auth;

public record ResetPasswordRequest(
  string Email,
  string Token,
  string NewPassword,
  string ConfirmNewPassword
);
