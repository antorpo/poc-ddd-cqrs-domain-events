using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmail(string email, string subject, string message)
        {
            await Task.Delay(5000);
            _logger.LogInformation("Email enviado!");
        }
    }
}
