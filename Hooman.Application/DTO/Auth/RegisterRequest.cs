namespace Hooman.Application.DTO.Auth;
public record RegisterRequest(
  string Email, 
  string UserName,
  string Password,
  string ConfirmPassword,
  string FirstName,
  string LastName,
  string? PhoneNumber
);
