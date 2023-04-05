using Core.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly string _email;
    private readonly string _host;
    private readonly string _password;

    public EmailService(IConfiguration configuration)
    {
        _host = configuration["EmailCredentials:Host"] ?? throw new Exception("Host is not specified");
        _email = configuration["EmailCredentials:Address"] ?? throw new Exception("Email address is not specified");
        _password = configuration["EmailCredentials:Password"] ?? throw new Exception("Email password is not specified");
    }

    public Task<bool> SendConfirmationEmailAsync(string email, string confirmationLink)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(_email[.._email.IndexOf('@')], _email));
        mailMessage.To.Add(new MailboxAddress(email[..email.IndexOf('@')], email));
        mailMessage.Subject = "Confirm your email";
        mailMessage.Body = new TextPart("plain")
        {
            Text = confirmationLink
        };

        using (var smtpClient = new SmtpClient())
        {
            smtpClient.Connect(_host, 465, true);
            smtpClient.Authenticate(_email, _password);
            var result = smtpClient.Send(mailMessage);
            smtpClient.Disconnect(true);
            if (result.Length > 0) return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}