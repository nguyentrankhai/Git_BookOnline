using DevExpress.Pdf;
using System;
using System.Windows.Media;

namespace DTO_Book
{
    public class Annotation
    {
        private string annotation_name;
        private int annotation_id;
        private string iduser;
        private string user_name;
        private string book_name;
        private string idbook;
        private int page;
        private string text;
        private int number;
        private string comment;
        private PdfTextMarkupAnnotationType annotation_type;
        private Color annotation_color;
        private DateTime creationDate;
        private DateTime modificationDate;
        private string subject;
        private double[] quart;
        public Annotation()
        {

        }
        public Annotation(int annotation_id, string iduser, string user_name, string book_name, string idbook, int page, string text, int number, PdfTextMarkupAnnotationType annotation_type, Color annotation_color, DateTime creationDate, DateTime modificationDate, string subject, double[] quart, string comment)
        {
            this.annotation_id = annotation_id;
            this.iduser = iduser;
            this.user_name = user_name;
            this.book_name = book_name;
            this.idbook = idbook;
            this.page = page;
            this.text = text;
            this.number = number;
            this.annotation_type = annotation_type;
            this.annotation_color = annotation_color;
            this.creationDate = creationDate;
            this.modificationDate = modificationDate;
            this.subject = subject;
            this.quart = quart;
            this.comment = comment;
        }
        public Annotation(string iduser, string user_name, string book_name, string idbook, int page, string text, int number, PdfTextMarkupAnnotationType annotation_type, Color annotation_color, DateTime creationDate, DateTime modificationDate, string subject, double[] quart, string comment)
        {            
            this.iduser = iduser;
            this.user_name = user_name;
            this.book_name = book_name;
            this.idbook = idbook;
            this.page = page;
            this.text = text;
            this.number = number;
            this.annotation_type = annotation_type;
            this.annotation_color = annotation_color;
            this.creationDate = creationDate;
            this.modificationDate = modificationDate;
            this.subject = subject;
            this.quart = quart;
            this.comment = comment;
        }
        public int Annotation_id
        {
            get
            {
                return annotation_id;
            }

            set
            {
                annotation_id = value;
            }
        }

        public string Iduser
        {
            get
            {
                return iduser;
            }

            set
            {
                iduser = value;
            }
        }

        public string User_name
        {
            get
            {
                return user_name;
            }

            set
            {
                user_name = value;
            }
        }

        public string Book_name
        {
            get
            {
                return book_name;
            }

            set
            {
                book_name = value;
            }
        }

        public string Idbook
        {
            get
            {
                return idbook;
            }

            set
            {
                idbook = value;
            }
        }

        public int Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        public PdfTextMarkupAnnotationType Annotation_type
        {
            get
            {
                return annotation_type;
            }

            set
            {
                annotation_type = value;
            }
        }

        public Color Annotation_color
        {
            get
            {
                return annotation_color;
            }

            set
            {
                annotation_color = value;
            }
        }

        public DateTime CreationDate
        {
            get
            {
                return creationDate;
            }

            set
            {
                creationDate = value;
            }
        }

        public DateTime ModificationDate
        {
            get
            {
                return modificationDate;
            }

            set
            {
                modificationDate = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        public double[] Quart
        {
            get
            {
                return quart;
            }

            set
            {
                quart = value;
            }
        }

        public string Annotation_name
        {
            get
            {
                return annotation_name;
            }

            set
            {
                annotation_name = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }

            set
            {
                comment = value;
            }
        }
    }
}
