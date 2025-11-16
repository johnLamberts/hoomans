
using System.Security.Cryptography;
using Hooman.Application.Interfaces;

namespace Hooman.Application.Services;

public class PasswordHasherService : IPasswordHasher
{
  private const int SaltSize = 32; // 256 Bits
  private const int HashSize = 32; // 256 Bits
  private const int Iterations = 100000; // PBKDF2 iterations

  public string HashPassword(string password, out string salt)
  {
    // Generate salt
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        salt = Convert.ToBase64String(saltBytes);

        // Generate hash
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var hashBytes = pbkdf2.GetBytes(HashSize);
        
        return Convert.ToBase64String(hashBytes);
  }

  public bool VerifyPassword(string password, string hash, string salt)
  {
    var saltBytes = Convert.FromBase64String(salt);
        var hashBytes = Convert.FromBase64String(hash);

        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var testHashBytes = pbkdf2.GetBytes(HashSize);

        return CryptographicOperations.FixedTimeEquals(hashBytes, testHashBytes);
  }
}
