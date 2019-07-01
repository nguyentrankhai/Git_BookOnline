
using System;
using System.Windows.Media.Imaging;

namespace DTO_BookOnline
{
    public class Book
    {
        #region Properties
        private string id;
        private string author;
        private string name;
        private string summary;
        private string year;
        private string catalog;
        private double price;
        private string img;
        private string note;
        private int page;
        private string url;
        private string trial_url;
        private DateTime lastview;
        private string status;
        private double rating;
        private int ratingCount;
        public BitmapImage ImageBook { get; set; }
        public DateTime LastView
        {
            get { return lastview; }
            set { lastview = value; }
        }
        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
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

        public string Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        public string Summary
        {
            get
            {
                return summary;
            }

            set
            {
                summary = value;
            }
        }

        public string Catalog
        {
            get
            {
                return catalog;
            }

            set
            {
                catalog = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
            }
        }

        public string Img
        {
            get
            {
                return img;
            }

            set
            {
                img = value;
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

        public string Url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public string Trial_url
        {
            get
            {
                return trial_url;
            }

            set
            {
                trial_url = value;
            }
        }

        public string Status
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

        public double Rating
        {
            get
            {
                return rating;
            }

            set
            {
                rating = value;
            }
        }

        public int RatingCount
        {
            get
            {
                return ratingCount;
            }

            set
            {
                ratingCount = value;
            }
        }

        #endregion

        #region Initalize 
        public Book(string id, string name, string author, string year, string catalog, string summary, double price)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.year = year;
            this.catalog = catalog;
            this.summary = summary;
        }

        public Book(string name, string catalog, string author, string year, string summary, double price, string url, string img, string note)
        {
            this.name = name;
            this.catalog = catalog;
            this.author = author;
            this.year = year;
            this.summary = summary;
            this.url = url;
            this.price = price;
            this.img = img;
            this.note = note;
            this.page = 0;
        }
        public Book(string id, string name, string catalog, string author, string year, string summary, double price, string url, string img, string note)
        {
            this.id = id;
            this.name = name;
            this.catalog = catalog;
            this.author = author;
            this.year = year;
            this.summary = summary;
            this.url = url;
            this.price = price;
            this.img = img;
            this.note = note;
            this.page = 0;
        }
        public Book(string id, string name, string catalog, string author, string year, string summary, double price, string img, string note, int page)
        {
            this.id = id;
            this.name = name;
            this.catalog = catalog;
            this.author = author;
            this.year = year;
            this.summary = summary;
            this.price = price;
            this.img = img;
            this.note = note;
            this.page = page;
        }
        public Book() { }
        #endregion
    }
}
