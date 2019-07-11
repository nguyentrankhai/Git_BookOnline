using DTO_Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline
{
    public class DAL_Publisher
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();

        public Publisher parseSQLtoDTO(tbl_Publisher pl)
        {
            Publisher p = new Publisher();
            if (pl != null)
            {
                p.Id = pl.PublisherID;
                p.Name = pl.PublisherName;
                p.Tel = pl.Tel;
                p.Address = pl.Address;
                p.Email = pl.E_mail;
                p.Fax = pl.Fax;
            }
            return p;
        }
        public List<Publisher> getlstPublisher(string bookid)
        {
            List<Publisher> lst = new List<Publisher>();
            if (bookid != null)
            {
                var lstpls = db.tbl_BookPublishers.Where(x => x.BookID == bookid);
                foreach (tbl_BookPublisher bp in lstpls)
                {
                    var pl = db.tbl_Publishers.Where(x => x.PublisherID == bp.PublisherID).SingleOrDefault();
                    if (pl != null)
                    {
                        lst.Add(parseSQLtoDTO(pl));
                    }
                }
            }
            return lst;
        }
    }
}
