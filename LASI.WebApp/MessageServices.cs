using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LASI.WebApp
{
    // This class is used by the application to send Email and SMS when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public static class MessageServices
    {
        public async static Task SendEmailAsync(string userEmail, string subject, string message)
        {
            var email = new SendGrid.SendGridMessage(
                from: new System.Net.Mail.MailAddress("thelasiproject@gmail.com"),
                to: new[] { new System.Net.Mail.MailAddress(userEmail) },
                subject: subject,
                html: message,
                text: message
            );
            var credentials = RetrieveCredentials();
            var transport = new SendGrid.Web(credentials);
            await transport.DeliverAsync(email);
            // Plug in your email service here to send an email.
        }
#pragma warning disable RECS0154 // Parameter is never used
        public static Task SendSmsAsync(string number, string message)
#pragma warning restore RECS0154 // Parameter is never used
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }

        private static System.Net.NetworkCredential RetrieveCredentials()
        {
            var configuration = new ConfigurationBuilder()
                //.AddUserSecrets()
                .Build();
            return new System.Net.NetworkCredential
            {
                UserName = configuration["SendGrid:Username"],
                Password = configuration["SendGrid:Password"]
            };
        }
    }
}
