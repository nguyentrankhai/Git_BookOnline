using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DTO_BookOnline
{
    public class Comment
    {
        private ImageSource usimage;
        private String userID;
        private String bookID;
        private String content;
        private DateTime date;
        private int status;
        private int rate;
        private String note;

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public string BookID
        {
            get
            {
                return bookID;
            }

            set
            {
                bookID = value;
            }
        }

        public string UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public ImageSource Image
        {
            get
            {
                return usimage;
            }

            set
            {
                usimage = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
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

        public int Rate
        {
            get
            {
                return rate;
            }

            set
            {
                rate = value;
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

        public Comment(ImageSource img, string bookID, string userID, string content, int status)
        {
            this.usimage = img;
            this.bookID = bookID;
            this.userID = userID;
            this.content = content;
            this.date = DateTime.Now;
            this.status = status;
        }
        public Comment() { }
        public Comment(string bookID, string userID, string content, int status)
        {
            this.bookID = bookID;
            this.userID = userID;
            this.content = content;
            this.date = DateTime.Now;
            this.status = status;
        }
    }
}
