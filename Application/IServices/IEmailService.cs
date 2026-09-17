namespace Application.IServices
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string toName, int userId, string token);
    }
}
