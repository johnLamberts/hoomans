
using System.Net;
using System.Net.Mail;
using Hooman.Application.Interfaces;
using Hooman.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hooman.Application.Services;

public class EmailService : IEmailService
{
  private readonly EmailSettings _emailSettings;
  private readonly ILogger<EmailService> _logger;

  public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
  {
    _emailSettings = emailSettings.Value;
    _logger = logger;
  }

  public async Task SendEmailAsync(string to, string subject, string htmlBody)
  {
     try
        {
            using var client = new SmtpClient(_emailSettings.SmtpHost, _emailSettings.SmtpPort)
            {
                EnableSsl = _emailSettings.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {To}", to);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {To}", to);
            throw;
        }
  }

   public async Task SendWelcomeEmailAsync(string to, string firstName, string verificationLink)
    {
        var subject = "Welcome to LMS - Verify Your Email";
        var htmlBody = GetWelcomeEmailTemplate(firstName, verificationLink);
        await SendEmailAsync(to, subject, htmlBody);
    }

    public async Task SendEmailVerificationAsync(string to, string firstName, string verificationLink)
    {
        var subject = "Verify Your Email Address";
        var htmlBody = GetEmailVerificationTemplate(firstName, verificationLink);
        await SendEmailAsync(to, subject, htmlBody);
    }

    public async Task SendPasswordResetAsync(string to, string firstName, string resetLink)
    {
        var subject = "Reset Your Password";
        var htmlBody = GetPasswordResetTemplate(firstName, resetLink);
        await SendEmailAsync(to, subject, htmlBody);
    }

    public async Task SendPasswordChangedAsync(string to, string firstName)
    {
        var subject = "Your Password Has Been Changed";
        var htmlBody = GetPasswordChangedTemplate(firstName);
        await SendEmailAsync(to, subject, htmlBody);
    }

    public async Task SendRoleAssignedAsync(string to, string firstName, string roleName)
    {
        var subject = $"New Role Assigned: {roleName}";
        var htmlBody = GetRoleAssignedTemplate(firstName, roleName);
        await SendEmailAsync(to, subject, htmlBody);
    }

    public async Task SendAccountActivatedAsync(string to, string firstName)
    {
        var subject = "Your Account Has Been Activated";
        var htmlBody = GetAccountActivatedTemplate(firstName);
        await SendEmailAsync(to, subject, htmlBody);
    }

    public async Task SendAccountDeactivatedAsync(string to, string firstName)
    {
        var subject = "Your Account Has Been Deactivated";
        var htmlBody = GetAccountDeactivatedTemplate(firstName);
        await SendEmailAsync(to, subject, htmlBody);
    }

       private string GetWelcomeEmailTemplate(string firstName, string verificationLink)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; padding: 12px 30px; background: #667eea; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Welcome to LMS! 🎉</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>Welcome to our Learning Management System! We're excited to have you join our community of learners and educators.</p>
        <p>To get started, please verify your email address by clicking the button below:</p>
        <center>
            <a href='{verificationLink}' class='button'>Verify Email Address</a>
        </center>
        <p>Or copy and paste this link into your browser:</p>
        <p style='background: #eee; padding: 10px; border-radius: 5px; word-break: break-all;'>{verificationLink}</p>
        <p><strong>This link will expire in 24 hours.</strong></p>
        <p>Once verified, you'll have full access to:</p>
        <ul>
            <li>Interactive learning materials</li>
            <li>AI-powered study assistance</li>
            <li>Progress tracking and analytics</li>
            <li>Collaborative learning tools</li>
        </ul>
        <p>If you didn't create this account, please ignore this email.</p>
        <p>Best regards,<br>The LMS Team</p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
        <p>You received this email because you registered for an account.</p>
    </div>
</body>
</html>";
    }

    private string GetEmailVerificationTemplate(string firstName, string verificationLink)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #667eea; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; padding: 12px 30px; background: #667eea; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Verify Your Email</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>Please verify your email address to complete your registration:</p>
        <center>
            <a href='{verificationLink}' class='button'>Verify Email</a>
        </center>
        <p>Or use this link: {verificationLink}</p>
        <p><strong>This link expires in 24 hours.</strong></p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
    </div>
</body>
</html>";
    }

    private string GetPasswordResetTemplate(string firstName, string resetLink)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #f56565; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; padding: 12px 30px; background: #f56565; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .warning {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Reset Your Password</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>We received a request to reset your password. Click the button below to create a new password:</p>
        <center>
            <a href='{resetLink}' class='button'>Reset Password</a>
        </center>
        <p>Or copy and paste this link into your browser:</p>
        <p style='background: #eee; padding: 10px; border-radius: 5px; word-break: break-all;'>{resetLink}</p>
        <div class='warning'>
            <strong>⚠️ Security Note:</strong>
            <ul>
                <li>This link will expire in 24 hours</li>
                <li>If you didn't request this, please ignore this email</li>
                <li>Your password won't change until you access the link above</li>
            </ul>
        </div>
        <p>For security reasons, if you continue to have problems, please contact our support team.</p>
        <p>Best regards,<br>The LMS Team</p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
    </div>
</body>
</html>";
    }

    private string GetPasswordChangedTemplate(string firstName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #48bb78; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .alert {{ background: #fed7d7; border-left: 4px solid #f56565; padding: 15px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>✓ Password Changed</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>This is a confirmation that your password has been successfully changed.</p>
        <div class='alert'>
            <strong>⚠️ Didn't make this change?</strong><br>
            If you didn't change your password, please contact our support team immediately and secure your account.
        </div>
        <p>For your security, we recommend:</p>
        <ul>
            <li>Use a strong, unique password</li>
            <li>Never share your password with anyone</li>
            <li>Enable two-factor authentication if available</li>
            <li>Change your password regularly</li>
        </ul>
        <p>If you have any concerns, please contact us immediately.</p>
        <p>Best regards,<br>The LMS Team</p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
    </div>
</body>
</html>";
    }

    private string GetRoleAssignedTemplate(string firstName, string roleName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #4299e1; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .role-badge {{ display: inline-block; background: #4299e1; color: white; padding: 8px 20px; border-radius: 20px; font-weight: bold; margin: 10px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>🎉 New Role Assigned!</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>Great news! You've been assigned a new role in the LMS:</p>
        <center>
            <div class='role-badge'>{roleName}</div>
        </center>
        <p>This role grants you access to additional features and capabilities. Log in to your account to explore your new permissions.</p>
        <p>If you have any questions about your new role, please contact your administrator.</p>
        <p>Best regards,<br>The LMS Team</p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
    </div>
</body>
</html>";
    }

    private string GetAccountActivatedTemplate(string firstName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #48bb78; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .button {{ display: inline-block; padding: 12px 30px; background: #48bb78; color: white; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>✓ Account Activated</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>Good news! Your account has been activated and you can now access the LMS.</p>
        <center>
            <a href='{_emailSettings.BaseUrl}/login' class='button'>Login Now</a>
        </center>
        <p>Welcome back! We're glad to have you.</p>
        <p>Best regards,<br>The LMS Team</p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
    </div>
</body>
</html>";
    }

    private string GetAccountDeactivatedTemplate(string firstName)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background: #718096; color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
        .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
        .footer {{ text-align: center; margin-top: 30px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='header'>
        <h1>Account Deactivated</h1>
    </div>
    <div class='content'>
        <p>Hi {firstName},</p>
        <p>Your account has been deactivated. You will no longer be able to access the LMS.</p>
        <p>If you believe this was done in error or if you'd like to reactivate your account, please contact our support team.</p>
        <p>Thank you for being part of our community.</p>
        <p>Best regards,<br>The LMS Team</p>
    </div>
    <div class='footer'>
        <p>© 2024 LMS. All rights reserved.</p>
    </div>
</body>
</html>";
    }
}

