using DevExpress.Pdf;
using System;
using System.Windows.Media;

namespace Template.Domain.Highlight
{
    public class Highlight
    {
        private string author;
        //private PdfRectangle bounds;
        private Color color;
        private string contents;
        private DateTimeOffset? creationDate;
        private DateTimeOffset? modificationDate;
        private double opacity;
        private string name;
        private string subject;
        private PdfTextMarkupAnnotationType markupType;
        //private Quadrilateral quads;
        private int pageNumber;
        private string bookName;
        private string colorString;

        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }

        //public PdfRectangle Bounds
        //{
        //    get
        //    {
        //        return bounds;
        //    }

        //    set
        //    {
        //        bounds = value;
        //    }
        //}

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public string Contents
        {
            get
            {
                return contents;
            }

            set
            {
                contents = value;
            }
        }

        public DateTimeOffset? CreationDate
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

        public DateTimeOffset? ModificationDate
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

        public double Opacity
        {
            get
            {
                return opacity;
            }

            set
            {
                opacity = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
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

        public PdfTextMarkupAnnotationType MarkupType
        {
            get
            {
                return markupType;
            }

            set
            {
                markupType = value;
            }
        }

        //public Quadrilateral Quads
        //{
        //    get
        //    {
        //        return quads;
        //    }

        //    set
        //    {
        //        quads = value;
        //    }
        //}
        public double QX1 { get; set; }
        public double QY1 { get; set; }
        public double QX2 { get; set; }
        public double QY2 { get; set; }
        public double QX3 { get; set; }
        public double QY3 { get; set; }
        public double QX4 { get; set; }
        public double QY4 { get; set; }


        public int PageNumber
        {
            get
            {
                return pageNumber;
            }

            set
            {
                pageNumber = value;
            }
        }

        public string BookName
        {
            get
            {
                return bookName;
            }

            set
            {
                bookName = value;
            }
        }

        public string ColorString
        {
            get
            {
                return colorString;
            }

            set
            {
                colorString = value;
            }
        }
    }
    public class Quadrilateral
    {
        public Quadrilateral(Pointer p1, Pointer p2, Pointer p3, Pointer p4) {
            this.P1 = p1;
            this.P2 = p2;
            this.P3 = p3;
            this.P4 = p4;
        }

        //
        // Summary:
        //     Gets the first quadrilateral point in the default user space.
        public Pointer P1 { get; set; }
        //
        // Summary:
        //     Gets the second quadrilateral point in the default user space.
        public Pointer P2 { get; set; }
        //
        // Summary:
        //     Gets the third quadrilateral point in the default user space.
        public Pointer P3 { get; set; }
        //
        // Summary:
        //     Gets the fourth quadrilateral point in the default user space.
        public Pointer P4 { get; set; }
    }
    public class Pointer
    {
        //
        // Summary:
        //     Initializes a new instance of the PdfPoint class with the specified coordinates.
        //
        // Parameters:
        //   x:
        //     A System.Double value. This value is assigned to the DevExpress.Pdf.PdfPoint.X
        //     property.
        //
        //   y:
        //     A System.Double value. This value is assigned to the DevExpress.Pdf.PdfPoint.Y
        //     property.
        public Pointer(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        //
        // Summary:
        //     Gets the X coordinate of the PdfPoint.
        public double X { get; set; }
        //
        // Summary:
        //     Gets the Y coordinate of the PdfPoint.
        public double Y { get; set; }
    }
}
