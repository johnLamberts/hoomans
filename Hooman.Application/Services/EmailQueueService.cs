using Microsoft.Extensions.Logging;

namespace Hooman.Application.Interfaces;
public class EmailQueueService : IEmailQueueService
{
    private readonly IEmailService _emailService;
    private readonly ILogger<EmailQueueService> _logger;

    public EmailQueueService(IEmailService emailService, ILogger<EmailQueueService> logger)
    {
        _emailService = emailService;
        _logger = logger;
    }

    public async Task QueueEmailAsync(string to, string subject, string htmlBody)
    {
        // Run email sending in background to not block the request
        _ = Task.Run(async () =>
        {
            try
            {
                await _emailService.SendEmailAsync(to, subject, htmlBody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send queued email to {To}", to);
            }
        });

        await Task.CompletedTask;
    }

}
