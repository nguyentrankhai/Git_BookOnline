using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_BookOnline
{
    public class Author
    {
        #region properties
        private string id;
        private string name;
        private string summary;//tóm tắt tiểu sử
        private DateTime date;
        private bool gen;
        private string img; //đường dẫn hình ảnh của tác giả
        private string note;
        private string country;
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

        public bool Gen
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

        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                country = value;
            }
        }
        #endregion

        #region Initalize
        public Author()
        {

        }
        public Author(string name, string summary, DateTime date, bool gen, string img, string note)
        {
            this.name = name;
            this.summary = summary;
            this.date = date;
            this.gen = gen;
            this.img = img;
            this.note = note;
        }
        #endregion
    }
}
