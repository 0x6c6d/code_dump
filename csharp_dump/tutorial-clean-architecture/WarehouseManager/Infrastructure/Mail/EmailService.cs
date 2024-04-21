using Application.Models.Mail;
using System.Net.Mail;

namespace Infrastructure.Mail;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public void SendEmail(Email email)
    {
        var smtpClient = new SmtpClient(_emailSettings.Host);

        try
        {
            using var message = new MailMessage(_emailSettings.Sender, _emailSettings.Recipient)
            {
                Subject = email.Subject,
                Body = email.Body
            };

            smtpClient.Send(message);
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
