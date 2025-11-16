namespace Hooman.Application.DTO.Auth;

public record LoginRequest(
  string EmailOrUserName,
  string Password,
  bool RememberMe = false
);
