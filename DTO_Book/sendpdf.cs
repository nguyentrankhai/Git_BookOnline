using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;

namespace BookOnline
{
    public class sendpdf
    {
        private Book book;

        public Book Book
        {
            get
            {
                return book;
            }

            set
            {
                book = value;
            }
        }

        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

       public sendpdf (Book book, string fileName)
        {
            this.book = book;
            this.fileName = fileName;
        }
        public sendpdf( string fileName)
        {
            this.fileName = fileName;
        }
        public sendpdf()
        { }
    }
}
