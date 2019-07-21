using STUBookOnline.Controllers;
using STUBookOnline.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STUBookOnline
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userID = Request.QueryString["a"];
            string email = Request.QueryString["b"];
            string pwd =  Request.QueryString["c"];
            pwd = EncodingText.Base64Decode(pwd);
            bool isVerify = String.IsNullOrEmpty(userID) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(pwd);
            if (!isVerify)
            {
                bool isActive = resetPassword(userID, email, pwd);
                string result = String.Format("Tài khoản {0} đã khôi phục mật khẩu thành công. Chúc bạn đọc sách vui vẻ", userID);
                lbResult.Text = result;
            }
        }
        private bool resetPassword(string userID, string email, string pwd)
        {
            try
            {
                using (BookOnlineEntities db = new BookOnlineEntities())
                {
                    tbl_Account tbl_Account = db.tbl_Account.Find(userID);
                    if (tbl_Account != null)
                    {
                        tbl_Account.EmailConfirmed = true;
                        tbl_Account.PWD = pwd;
                        tbl_Account.Status = 1;
                        db.Entry(tbl_Account).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }

            }
            catch (Exception e)
            {
                e.ToString();
            }
            return true;
        }
    }
}