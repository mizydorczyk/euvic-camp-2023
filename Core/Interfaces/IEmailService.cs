namespace Core.Interfaces;

public interface IEmailService
{
    Task<bool> SendConfirmationEmailAsync(string email, string confirmationLink);
}