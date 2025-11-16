namespace Hooman.Application.DTO.Auth;

public record RefreshTokenRequest(
  string AccessToken,
  string RefreshToken
);
