namespace Backend.Services
{
    public interface IEmailSender
    {
        public  Task SendEmailAsync(string email,string subject,string body);
    }
    
}