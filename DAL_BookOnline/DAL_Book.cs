using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;
using System.Windows.Media.Imaging;
using System.Collections;

namespace DAL_BookOnline
{
    public class DAL_Book
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();

        private Book getRatingofBook(Book sach)
        {
            DAL_Rating dal_rating = new DAL_Rating();
            List<Rating> ratings = dal_rating.getListRatingWithBookID(sach.Id);
            foreach (Rating rating in ratings)
            {
                sach.Rating = rating.Rate + sach.Rating;
            }
            sach.RatingCount = ratings.Count;
            sach.Rating = (sach.Rating / sach.RatingCount);
            return sach;
        }
        private Book baseSQLtoDTO(tbl_Book book) {
            if (book != null)
            {

                Book sach = new Book();
                sach.Author = book.AUTHOR;
                sach.Catalog = book.catalo;
                sach.Id = book.BookID;
                sach.Name = book.BookNAME;
                sach.Year = book.YEAR.ToString();
                sach.Url = book.URL;
                sach.Trial_url = book.TRIAL_URL;
                sach.Summary = book.SUMMARY;
                sach.Img = book.IMG;
                sach.ImageBook = (BitmapImage)ConverterImage.convertImage(book.IMG);
                sach.Note = book.NOTE;
                Object o = book.PRICE;
                if (o == null)
                { o = 0; }
                double d = new double();
                d = double.Parse(o.ToString());
                sach.Price = d;
                DAL_Language dal = new DAL_Language();
                sach.Language = dal.getLanguage(book.LangID);
                DAL_Publisher dalp = new DAL_Publisher();
                sach.LstPbl = dalp.getlstPublisher(sach.Id);
                return getRatingofBook(sach);
            }
            return null;
        }

        public List<Book> getAllBook()
        {
            var books = db.tbl_Books.ToList();
            if (books == null) return null;

            List<Book> lstBook = new List<Book>();
            foreach(tbl_Book book in books)
            {
              
                lstBook.Add(baseSQLtoDTO(book));
            }
            return lstBook;
        }

        public List<Book> getBookWithCatalog(string str)
        {
            List<Book> lstBook = new List<Book>();
            var books = db.tbl_Books.Where(x => x.catalo == str);
            if (books == null) return null;
            foreach(tbl_Book book in books)
            {
                lstBook.Add(baseSQLtoDTO(book));
            }
            return lstBook;
        }

        public List<Book> getBookWithAuthor(string str)
        {
            List<Book> lstBook = new List<Book>();
            var books = db.tbl_Books.Where(x => x.AUTHOR == str);
            if (books == null) return null;
            foreach(tbl_Book book in books)
            {
                lstBook.Add(baseSQLtoDTO(book));
            }
            return lstBook;
        }

        public Book findBook(string str)
        {
            var books = db.tbl_Books.Where(x => x.BookID == str).SingleOrDefault();
            if (books == null) return null;
            Book sach = new Book();
            return baseSQLtoDTO(books);
        }

        public List<Book> getBookWithString(string str)
        {
            List<Book> lstBook = new List<Book>();
            var books = db.tbl_Books.Where(x => x.BookNAME.Contains(str)).ToList();
            if (books == null) return null;

            foreach (tbl_Book book in books)
            {
                lstBook.Add(baseSQLtoDTO(book));
            }
            return lstBook;
        }

        public List<Book> getBooksOfUser(User user)
        {
            List<Book> lstBook = new List<Book>();
            var books = db.tbl_BookofUsers.Where(x => x.USERID == user.ID1).GroupBy(p => p.BOOKID).Select(c => c.FirstOrDefault());
            if (books == null) return null;
            foreach (tbl_BookofUser book in books)
            {
                Book bk = findBook(book.BOOKID);
                if (bk != null)
                {
                    bk.Status = book.Status.ToString();
                    bk.Page = (int)book.PAGE;
                    bk.LastView =(DateTime) book.LastView;
                    lstBook.Add(bk);
                }
            }
            return lstBook;
        }

        public bool updatePage(User user, Book book, int page)
        {
            try
            {
                tbl_BookofUser sach = db.tbl_BookofUsers.Single(b => b.USERID == user.ID1 && b.BOOKID == book.Id);
                sach.PAGE = page;
                sach.LastView = DateTime.Today;
                db.SubmitChanges();
                //ExcuteNonQuery("UPDATE bookofuser set page=" + page + " where userid=" + user.ID1 + " and bookid=" + book.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool insertBookOfUser(User user, Book book)
        {
            try
            {                
                tbl_BookofUser sach = new tbl_BookofUser();
                sach.ID = db.tbl_BookofUsers.Count() + 1;
                sach.BOOKID = book.Id;
                sach.USERID = user.ID1;
                sach.PAGE = book.Page;
                sach.LastView = DateTime.Today;
                sach.Status = book.Status[0];
                db.tbl_BookofUsers.InsertOnSubmit(sach);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool insertBook(Book book)
        {
            try
            {
                tbl_Book sach = new tbl_Book();
                sach.BookID = book.Id;
                sach.BookNAME = book.Name;
                sach.AUTHOR = book.Author;
                sach.catalo = book.Catalog;
                sach.LangID = book.Language.LangID;
                sach.YEAR = Int32.Parse(book.Year);
                sach.SUMMARY = book.Summary;
                sach.PRICE = decimal.Parse( book.Price +"");
                sach.URL = book.Url;
                sach.TRIAL_URL = book.Trial_url;
                sach.NOTE = book.Note;
                sach.DateUpdate = DateTime.Now;
                db.tbl_Books.InsertOnSubmit(sach);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateBookOfUser(User user , Book book)
        {
            try
            { //db.tbl_BookofUsers.Where(x => x.USERID == user.ID1 && x.BOOKID == book.Id).Select(a => a).Take(1);
                tbl_BookofUser sach = (from tbl in db.tbl_BookofUsers where tbl.USERID == user.ID1 && tbl.BOOKID == book.Id select tbl).First();
                sach.PAGE = book.Page;
                sach.NOTE = book.Note;
                sach.Status = Char.Parse(book.Status);
                sach.LastView = DateTime.Now;
                db.SubmitChanges();
                return true;
            }
            catch(Exception ex) {
                return false;
            }
        }

        public bool removeBookOfUser(string idBook,string idUser, char status)
        {
            try
            {
                tbl_BookofUser book = db.tbl_BookofUsers.Single(x => x.BOOKID == idBook && x.USERID == idUser && x.Status==status);
                db.tbl_BookofUsers.DeleteOnSubmit(book);
                db.SubmitChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool findBookofUser(string usID, string bkID, char status)
        {
            //var books = db.tbl_BookofUsers.Where(x => x.BOOKID == bkID && x.USERID == usID)//
            var books = (from x in db.tbl_BookofUsers where x.BOOKID == bkID && x.USERID == usID && x.Status == status  select x).FirstOrDefault();

            if (books == null)
                return false;
            else return true;
        }
        public bool findBook(string usID, string bkID)
        {
            //var books = db.tbl_BookofUsers.Where(x => x.BOOKID == bkID && x.USERID == usID)//
            var books = (from x in db.tbl_BookofUsers where x.BOOKID == bkID && x.USERID == usID select x).FirstOrDefault();

            if (books == null)
                return false;
            else return true;
        }
        public void insertBookOfUserFromAPI(List<tbl_BookofUser> lst, string userID)
        {
            IEnumerable<tbl_BookofUser> lstBookOfUserDel = (from x in db.tbl_BookofUsers where x.USERID == userID select x).ToList();
            db.tbl_BookofUsers.DeleteAllOnSubmit(lstBookOfUserDel);
            db.tbl_BookofUsers.InsertAllOnSubmit(lst);
            db.SubmitChanges();
        }

        public void insertBookStoreFromAPI(List<tbl_Book> lstInsert)
        {
            var arr = lstInsert.Select(x => x.BookID);
            
            IEnumerable<tbl_Book> lstDel = db.tbl_Books.Where(x => arr.ToArray().Contains(x.BookID));
            db.tbl_Books.DeleteAllOnSubmit(lstDel);
            db.tbl_Books.InsertAllOnSubmit(lstInsert);
            db.SubmitChanges();
        }
    }
}
