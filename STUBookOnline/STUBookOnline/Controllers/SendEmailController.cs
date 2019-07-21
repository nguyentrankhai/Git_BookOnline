using System;
using System.Configuration;
using System.Net.Mail;
using System.Web.Http;


namespace STUBookOnline.Controllers
{
    public class SendEmailController : ApiController
    {
        public string getRegister(string userid, string email)
        {
            if (String.IsNullOrEmpty(email))
                return "Not Found";

            if (checkValidEmail(email) == true)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);
                    //mail.To.Add("xxx@gmail.com");
                    string mailaddress = ConfigurationManager.AppSettings["EMAIL"];
                    string mailpasss = ConfigurationManager.AppSettings["PWD"];
                    mail.From = new MailAddress(mailaddress);


                    mail.Subject = "Xác nhận Email";
                    string local = ConfigurationManager.AppSettings["LOCALHOST"];
                    mail.Body = "Chào mừng bạn đến với BookOnline! \nVui lòng truy cập vào link sau để hoàn tất việc đăng ký.\n" + local +"ActiveEmail.aspx?userid=" + userid + "&email=" + email;

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         (mailaddress, mailpasss); // ***use valid credentials***
                    smtp.Port = 587;

                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return "Success";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Email not Valid";
            }
        }

        public string getResetPass(string userid, string email, string password)
        {
            if (String.IsNullOrEmpty(email))
                return "Not Found";

            if (checkValidEmail(email) == true)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.To.Add(email);
                    //mail.To.Add("xxx@gmail.com");
                    string mailaddress = ConfigurationManager.AppSettings["EMAIL"];
                    string mailpasss = ConfigurationManager.AppSettings["PWD"];
                    mail.From = new MailAddress(mailaddress);


                    mail.Subject = "Khôi phục Mật Khẩu";
                    string local = ConfigurationManager.AppSettings["LOCALHOST"];
                    mail.Body = "Vui lòng truy cập vào link sau để đặt lại mật khẩu của bạn." + local +"ResetPassword.aspx?a=" + userid + "&b=" + email+ "&c="+password ;

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         (mailaddress, mailpasss); // ***use valid credentials***
                    smtp.Port = 587;

                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    return "Success";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Email not Valid";
            }
        }
        private bool checkValidEmail(string email)
        {
            try
            {
                MailAddress mail = new MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //BookOnlineEntities db = new BookOnlineEntities();
    }
}
