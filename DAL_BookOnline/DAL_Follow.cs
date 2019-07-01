using DTO_Book;
using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline
{
    public class DAL_Follow
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();

        public bool insertFollow( User follower, User user)
        {
            bool result = false;
            try
            {
                tbl_Follow follow = new tbl_Follow();
                follow.AccountID1 = follower.ID1; 
                follow.AccountID2 = user.ID1;
                follow._Status = 1;
                db.tbl_Follows.InsertOnSubmit(follow);
                
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return result;
        }

        private Follow parseLinQtoDTO(tbl_Follow flw)
        {
            Follow f = new Follow();
            f.AccountID1 = flw.AccountID1;
            f.AccountID2 = flw.AccountID2;
            f.Status = flw._Status;
            return f;
        }
        public Follow getFollow(string followerID, string userID)
        {
            try
            {
                var flw = db.tbl_Follows.Where(n => n.AccountID1 == followerID && n.AccountID2 == userID).SingleOrDefault();
                if (flw != null)
                {
                    return parseLinQtoDTO(flw);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return null;
        }
        public bool updateFollow(User follower, User user)
        {
            bool result = false;
            //try
            {
                tbl_Follow f = db.tbl_Follows.Single(n => n.AccountID1 == follower.ID1 && n.AccountID2 == user.ID1);

                if (f != null)
                {
                    if (f._Status == 1)
                    {
                        f._Status = 0;
                    }
                    else
                    { f._Status = 1; }
                    //db.tbl_Follows.InsertOnSubmit(f);
                    db.SubmitChanges();
                    result = true;
                }
            }
            //catch (Exception ex)
            //{
            //    System.Console.Write(ex.Message);
            //}
            return result;
        }

        public List<User> getListFollowing(User follower)
        {
            List<User> lst = new List<User>();
            var flws = db.tbl_Follows.Where(x => x.AccountID1 == follower.ID1 && x._Status == 1);
            if (flws == null) return null;
            foreach(tbl_Follow f in flws )
            {
                DAL_User dal_user = new DAL_User();
                lst.Add(dal_user.getUser(f.AccountID2));
            }
            return lst;
        }
        public List<User> getListFollower(User following)
        {
            List<User> lst = new List<User>();
            var flws = db.tbl_Follows.Where(x => x.AccountID2 == following.ID1 && x._Status == 1);
            if (flws == null) return null;
            foreach (tbl_Follow f in flws)
            {
                DAL_User dal_user = new DAL_User();
                lst.Add(dal_user.getUser(f.AccountID1));
            }
            return lst;
        }
    }
}
