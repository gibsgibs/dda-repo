using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ddaproj.Services
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }
        public EmailSender(IOptions<AuthMessageSenderOptions> options)
        {
            Options = options.Value;
        }
        public Task Excecute(string apiKey, string email, string subject, string message)
        {
            var client = new SendGridClient(apiKey);
            var sendGridMessage = new SendGridMessage()
            {
                From = new EmailAddress("noreply@oregondda.org", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            sendGridMessage.AddTo(new EmailAddress(email));
            sendGridMessage.SetClickTracking(false, false);
            return client.SendEmailAsync(sendGridMessage);
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Excecute(Options.SendGridKey, email, subject, message);
        }
    }
}
