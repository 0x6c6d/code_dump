namespace Application.Contracts.Infrastructure;
public interface IEmailService
{
    void SendEmail(Email email);
}
