using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;
using DAL_BookOnline;
namespace BUS_BookOnline
{
    public class BUS_User
    {
        DAL_User dalUser = new DAL_User();
        public bool buyBook(User user, Book book)
        {
            return dalUser.buyBook(user, book);
        }
        public bool insertUser(User user)
        {
            //return dalUser.insertUser(user);
            return dalUser.insertUser(user);
        }
        public bool updateUser(User user, string pathAvatar)
        {
            //return dalUser.insertUser(user);
            return dalUser.updateUser(user, pathAvatar);
        }
        public User getUser(String ID)
        {
            //  return dalUser.getUser(user);
            if (dalUser.getUser(ID) != null)
            {
                return dalUser.getUser(ID);
            }
            return null;
        }
    }
}
