using InstaClone.Domain.Interfaces;
using InstaClone.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace InstaClone.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailSenderSettings _emailSettings;

    public EmailSender(IOptions<EmailSenderSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(_emailSettings.ApiKey))
        {
            throw new Exception("ERROR: SendGrid API Key not configured in appsettings.json.");
        }

        var client = new SendGridClient(_emailSettings.ApiKey);
        var from = new EmailAddress(_emailSettings.FromEmail, _emailSettings.FromName);
        var to = new EmailAddress(email);

        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

        var response = await client.SendEmailAsync(msg);
    }
}