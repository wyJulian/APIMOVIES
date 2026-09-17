using Application.IServices;
using APIMOVIES.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace APIMOVIES.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _settings;

        public EmailService(IOptions<EmailSetting> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailConfirmationAsync(string toEmail, string toName, int userId, string token)
        {
            var confirmationLink = $"{_settings.ConfirmationUrlBase}?userId={userId}&token={Uri.EscapeDataString(token)}";

            var templatePath = Path.Combine(AppContext.BaseDirectory, "EmailTemplates", "EmailConfirmation.html");
            var body = await File.ReadAllTextAsync(templatePath);
            body = body.Replace("{{UserName}}", toName).Replace("{{ConfirmationLink}}", confirmationLink);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromAddress));
            message.To.Add(new MailboxAddress(toName, toEmail));
            message.Subject = "Confirma tu cuenta";
            message.Body = new TextPart("html") { Text = body };

            using var client = new SmtpClient();
            await client.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.Username, _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
