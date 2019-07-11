using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline
{
    public class DAL_Author
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();
        public List<Author> getAllAuthor()
        {
            List<Author> lstAuthor = new List<Author>();
            try
            {
                var auths = db.tbl_Authors.ToList();
                if (auths == null) return null;

                List<Author> lstauth = new List<Author>();
                foreach (tbl_Author auth in auths)
                {
                    lstauth.Add(parseSQLtoDTO(auth));
                }
                return lstauth;
            }
            catch
            {

            }
            return lstAuthor;
        }
        private Author parseSQLtoDTO(tbl_Author o)
        {
            Author auth = new Author();
            if (o != null)
            {
                auth.Name = o.AuthorNAME;
                auth.Id = o.AuthorID;
            }
            return auth;
        }
        public bool insertAuthor(Author auth)
        {
            bool result = false;
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Author us = new tbl_Author();
                us.AuthorID = auth.Id;
                us.AuthorNAME = auth.Name;
                us.BIRTHDAY = auth.Date;
                us.COUNTRY = auth.Country;
                us.GEN = auth.Gen;
                us.SUMMARY = auth.Summary;
                us.IMG = auth.Img;
                us.NOTE = auth.Note;
                context.tbl_Authors.InsertOnSubmit(us);
                context.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return result;
        }
    }
}
