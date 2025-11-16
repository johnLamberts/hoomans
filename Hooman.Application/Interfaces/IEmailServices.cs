namespace Hooman.Application.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string htmlBody);
    Task SendWelcomeEmailAsync(string to, string firstName, string verificationLink);
    Task SendEmailVerificationAsync(string to, string firstName, string verificationLink);
    Task SendPasswordResetAsync(string to, string firstName, string resetLink);
    Task SendPasswordChangedAsync(string to, string firstName);
    Task SendRoleAssignedAsync(string to, string firstName, string roleName);
    Task SendAccountActivatedAsync(string to, string firstName);
    Task SendAccountDeactivatedAsync(string to, string firstName);
}
