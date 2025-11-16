using System.Security.Claims;
using Hooman.Domain.Entities.Identity;

namespace Hooman.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, IEnumerable<string> roles, IEnumerable<string> permissions);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
    string GetJwtIdFromToken(string token);
}
