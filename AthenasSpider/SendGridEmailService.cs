using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace AthenasSpider
{
    internal class SendGridEmailService : IIdentityMessageService
    {
      
            public Task SendAsync(IdentityMessage message)
            {
                string sendGridApiKey = System.Configuration.ConfigurationManager
                        .AppSettings["SendGrid.ApiKey"];

                SendGrid.SendGridClient client =
                    new SendGrid.SendGridClient(sendGridApiKey);

                SendGrid.Helpers.Mail.SendGridMessage sendgridMessage =
                    new SendGrid.Helpers.Mail.SendGridMessage();

                sendgridMessage.AddTo(message.Destination);
                sendgridMessage.Subject = message.Subject;
                sendgridMessage.SetFrom("no-reply@athenasspider.com", "Athena's Spider Administrator");

                sendgridMessage.AddContent("text/html", message.Body);

                sendgridMessage.SetTemplateId("eaf676b4-4d36-42f9-8961-de31f52ecfc3");

                return client.SendEmailAsync(sendgridMessage);

            }
        }
    }
