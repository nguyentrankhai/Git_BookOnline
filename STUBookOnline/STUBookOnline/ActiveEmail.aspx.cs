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
    public partial class ActiveEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string userID = Request.QueryString["userid"];
            string email = Request.QueryString["email"];
            bool isVerify = String.IsNullOrEmpty(userID) || String.IsNullOrEmpty(email);
            if (!isVerify)
            {
                bool isActive = activeUser(userID, email);
                string result = String.Format("Tài khoản {0} đã được kích hoạt thành công. \nEmail của bạn là: {1}", userID, email);
                lbResult.Text = result;
            }
        }

        private bool activeUser(string userID, string email)
        {
            try
            {
                using (BookOnlineEntities db = new BookOnlineEntities())
                {
                    tbl_Account tbl_Account = db.tbl_Account.Find(userID);
                    if (tbl_Account != null)
                    {
                        tbl_Account.EmailConfirmed = true;
                        tbl_Account.Email = email;
                        tbl_Account.SignupDate = DateTime.Now;
                        tbl_Account.Status = 1;
                        db.Entry(tbl_Account).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
               
            }
            catch(Exception e)
            {
                e.ToString();
            }
            return true;
        }
    }
}