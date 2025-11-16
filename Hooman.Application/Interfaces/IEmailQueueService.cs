namespace Hooman.Application.Interfaces;
public interface IEmailQueueService
{
    Task QueueEmailAsync(string to, string subject, string htmlBody);
}
