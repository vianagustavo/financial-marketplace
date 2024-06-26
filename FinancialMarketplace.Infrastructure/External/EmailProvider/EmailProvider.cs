using System.Globalization;
using System.Net;
using System.Net.Mail;

using FinancialMarketplace.Application.Contracts.External;
using FinancialMarketplace.Application.Exceptions;

using Microsoft.Extensions.Configuration;

namespace FinancialMarketplace.Infrastructure.External;

public class EmailProvider : IEmailProvider
{
    private readonly SmtpClient _smtpClient;
    private readonly string _emailSender;

    public EmailProvider(IConfiguration configuration, SmtpClient smtpClient)
    {
        _emailSender = configuration["EMAIL_SENDER"] ?? throw new MissingEnvironmentVariableException("EMAIL_SENDER");
        _smtpClient = smtpClient;
        ConfigureSmtpClient(configuration);
    }

    private void ConfigureSmtpClient(IConfiguration configuration)
    {
        _smtpClient.Host = configuration["SMTP_HOST"] ?? throw new MissingEnvironmentVariableException("SMTP_HOST");
        _smtpClient.Port = int.Parse(configuration["SMTP_PORT"] ?? throw new MissingEnvironmentVariableException("SMTP_PORT"), CultureInfo.InvariantCulture);
        _smtpClient.Credentials = new NetworkCredential(configuration["SMTP_USER"] ?? throw new MissingEnvironmentVariableException("SMTP_USER"), configuration["SMTP_PASSWORD"] ?? throw new MissingEnvironmentVariableException("SMTP_PASSWORD"));
        _smtpClient.EnableSsl = true;
    }


    public async Task Send(string to, string subject, string body)
    {
        var message = new MailMessage
        {
            From = new MailAddress(_emailSender),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        message.To.Add(to);

        await _smtpClient.SendMailAsync(message);

        Console.WriteLine($"Email sent to {to}");

        message.Dispose();
    }
}
