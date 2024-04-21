namespace Application.Models.Mail;
public class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public string Sender { get; set; } = string.Empty;
    public string Recipient { get; set; } = string.Empty;
}
