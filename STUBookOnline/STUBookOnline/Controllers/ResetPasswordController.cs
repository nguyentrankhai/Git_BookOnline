using RazorEngine;
using STUBookOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace STUBookOnline.Controllers
{
    public class ResetPasswordController : ApiController
    {
        BookOnlineEntities db = new BookOnlineEntities();

     
        private bool tbl_AccountExists(string id)
        {
            return db.tbl_Account.Count(e => e.AccountID == id) > 0;
        }

        public HttpResponseMessage get(string userid, string email, string password)
        {
            var mess = "";
            tbl_Account tbl_Account = db.tbl_Account.Find(userid);
            if (tbl_Account == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound); ;
            }

            tbl_Account.PWD = password;

            db.Entry(tbl_Account).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                dynamic model = new { Name = "Đã thay đổi mật khẩu thành công!", Subtitle = "Hãy đăng nhập vào ứng dụng để bắt đầu trải nghiệm!" };
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                string viewPath = HttpContext.Current.Server.MapPath(@"~/Views/Customize/ActivedAccount.cshtml");
                var template = File.ReadAllText(viewPath);
                string parseView = Razor.Parse(template, model);
                response.Content = new StringContent(parseView);
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AccountExists(userid))
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound); ;
                }
                else
                {
                    throw;
                }
            }

        }
    }
}
