using DAL_BookOnline;
using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_BookOnline
{
    public class BUS_Author
    {
        DAL_Author dal = new DAL_Author();
        public List<Author> getAllAuthor()
        {
            return dal.getAllAuthor();
        }
        public bool insertAuthor( Author auth)
        {
            return dal.insertAuthor(auth);
        }
    }
}
