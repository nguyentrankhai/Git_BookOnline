using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_BookOnline;
using DTO_BookOnline;
namespace BUS_BookOnline
{
    public class BUS_Book
    {
        #region Book
        DAL_Book dalBook = new DAL_Book();

        public List<Book> getAllBook()
        {
            return dalBook.getAllBook();
        }
        //lấy sách theo chuỗi s
        public List<Book> getBook(String s)
        {
            return dalBook.getBookWithString(s);
        }
        public Book getBookWithID(String s)
        {
            return dalBook.findBook(s);
        }
        #endregion

        #region Book-User

        public List<Book> getBookOfUser(User user)
        {
            return dalBook.getBooksOfUser(user);
        }
        public bool updatePage(User user, Book book, int page)
        {
            return dalBook.updatePage(user, book, page);
        }
        public bool updateBookOfUser(User user, Book book)
        {
            return dalBook.updateBookOfUser(user, book);
        }
        //public bool checkbook(int idbk, int idus)
        //{
        //    return dalBook.checkbook(idbk, idus);
        //}
        public bool removeBookofUser(string idbk, string idus, string status)
        {
            return dalBook.removeBookOfUser(idbk, idus, char.Parse(status));
        }
        public bool insertBookOfUser(User user, Book book)
        {
            if (dalBook.findBook(user.ID1, book.Id,char.Parse( book.Status))!= false) { 
                return dalBook.insertBookOfUser(user, book);
            }
            else { 
                return dalBook.updateBookOfUser(user, book);
            }
            return false;
        }
       
        #endregion

        #region Book-Catalog
        public List<Book> getBookwithCatalo(string s)
        {
            return dalBook.getBookWithCatalog(s);
        }
        #endregion
        public List<Book> getBookwithAuthor(string s)
        {
            return dalBook.getBookWithAuthor(s);
        }
        

    }
}
