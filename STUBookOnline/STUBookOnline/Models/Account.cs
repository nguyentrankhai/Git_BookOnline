using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STUBookOnline.Models
{
    public class Account
    {
        public string AccountID { get; set; }
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public DateTime SignupDate
        {
            get
            {
                return signupDate;
            }

            set
            {
                signupDate = DateTime.Now ;
            }
        }

        public bool Status
        {
            get
            {
                return status;
            }

            set
            {
                status = true;
            }
        }

        private DateTime signupDate;
        private bool status;


    }
}