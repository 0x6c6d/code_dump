namespace Application.Models.Mail;
public class Email
{
    public EmailSettings Settings { get; set; } = new();
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
}
