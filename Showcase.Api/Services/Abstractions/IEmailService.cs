namespace Showcase.Api.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendPasswordResetTokenAsync(string to, string token, CancellationToken cancellationToken = default);
    }
}