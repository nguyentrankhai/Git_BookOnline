using System;
using System.Windows.Media;

namespace DTO_BookOnline
{
    public class User
    {
        private string ID;
        private string username;
        private string password;
        private ImageSource image;

        private int status;
        private double wallet;
        private string gen;
        private DateTime remaining;
        private string note;
        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string ID1
        {
            get
            {
                return ID;
            }

            set
            {
                ID = value;
            }
        }



        public ImageSource Image1
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }

        public double Wallet
        {
            get
            {
                return wallet;
            }

            set
            {
                wallet = value;
            }
        }

        public string Gen
        {
            get
            {
                return gen;
            }

            set
            {
                gen = value;
            }
        }

        public DateTime Remaining
        {
            get
            {
                return remaining;
            }

            set
            {
                remaining = value;
            }
        }

        public string Note
        {
            get
            {
                return note;
            }

            set
            {
                note = value;
            }
        }

        public User(string id, string username, string pwd)
        {
            this.ID1 = id;
            this.Username = username;
            this.Password = pwd;
        }
        public User(string id, string username, string pwd, ImageSource img)
        {
            this.ID1 = id;
            this.Username = username;
            this.Password = pwd;
            this.Image1 = img;
        }
        public User(string username, string pwd)
        {
            this.Username = username;
            this.Password = pwd;
        }
        public User()
        {
            this.ID = "";
            this.username = "";
            this.password = "";
        }
    }
}
