using DevExpress.Pdf;
using System;
using System.Windows.Media;

namespace DTO_BookOnline
{
    public class HighLightData
    {
        private int annotation_id;
        private string iduser;
        private string user_name;
        private string book_name;
        private string idbook;
        private int page;
        private string text;
        private int number;
        private PdfTextMarkupAnnotationType annotation_type;
        private Color annotation_color;
        private DateTime creationDate;
        private DateTime modificationDate;
        private string subject;
        private double[] quart;

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
    }
}
