using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AssessmentApp.WebClient.Code
{
    public class Emailer
    {
        public static async Task SendEmail(string to, string subject, string body) {
            


            var myMessage = new SendGrid.SendGridMessage();
            myMessage.AddTo(to);
            myMessage.From = new System.Net.Mail.MailAddress("donotreply@AssessmentApp.com", "Assessment Application");
            myMessage.Subject = subject;
            myMessage.Text = body;

            var transportWeb = new SendGrid.Web(AppSettings.SendGridApiKey);
            await transportWeb.DeliverAsync(myMessage); 
        }
    }
}