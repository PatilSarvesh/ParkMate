using System.Net;
using System.Net.Mail;
using Backend.Models;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
    public class EmailSender : IEmailSender
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion

        #region Ctor
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        #endregion

        #region Methods
        public async Task SendEmailAsync(string email,string subject,string body)
        {
            try
            {
                var mail = _emailSettings.Mail;
                var pw = _emailSettings.Password;
    
                var client = new SmtpClient(_emailSettings.Host,_emailSettings.Port)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mail,pw)
                };
                await client.SendMailAsync( new MailMessage(from: mail, to: email, subject, body));
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
        #endregion

    }
}