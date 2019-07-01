using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_BookOnline;
using DTO_BookOnline;

namespace BUS_BookOnline
{
    public class BUS_Follow
    {
        DAL_Follow dal = new DAL_Follow();

        public bool insertFollow(User follower, User user)
        {
            bool result = false;
            if(dal.getFollow(follower.ID1, user.ID1)==null)
            {
                result = dal.insertFollow(follower, user);
            }
            return result;
        }

        public bool updateFollow(User follower, User user)
        {
            bool result = false;
            if (dal.getFollow(follower.ID1, user.ID1) != null)
            {
                result = dal.updateFollow(follower, user);
            }
            return result;
        }

        public List<User> getListFollowing(User follower)
        {
            return dal.getListFollowing(follower);
        }
        public List<User> getListFollower(User following)
        {
            return dal.getListFollower(following);
        }

        public Follow getFlw(User follower, User following)
        {
            return dal.getFollow(follower.ID1, following.ID1);
            
        }
    }
}
