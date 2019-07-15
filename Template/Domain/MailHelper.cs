using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Mime;

namespace Common
{
    public  class MailHelper
    {
        public  void SendMail(string toEmailAddress)
        {
            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress("toEmailAddress", ""));

            // From
            mailMsg.From = new MailAddress("trinh199724@gmail.com", "BookOnline Application");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "Xác nhận Email"; // Tiêu đề maill
            string text = "Xác nhận Email hello"; // Nội dung mail dạng Text
            //string html = @"<p>html body</p>"; // Nội dung mail dạng HTML
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            //mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("trinh.lam9727@gmail.com", "a123456789");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);


            //var myMessage = new SendGrid.SendGridMessage();
            //myMessage.AddTo("test@sendgrid.com");
            //myMessage.From = new EmailAddress("you@youremail.com", "First Last");
            //myMessage.Subject = "Sending with SendGrid is Fun";
            //myMessage.PlainTextContent = "and easy to do anywhere, even with C#";

            //var transportWeb = new SendGrid.Web("SENDGRID_APIKEY");
            //transportWeb.DeliverAsync(myMessage);
        }
    }
}
