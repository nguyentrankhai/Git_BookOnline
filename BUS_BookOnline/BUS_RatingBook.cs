using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_BookOnline;
using DTO_BookOnline;

namespace BUS_BookOnline
{
    public class BUS_RatingBook
    {
        DAL_Rating dal = new DAL_Rating();


        /// <summary>
        /// INSERT RATING ;
        /// Add rating on condition that it does not exist
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool insertRating(Rating rating)
        {
            Rating checkExist = dal.findRating(rating);
            //Add rating on condition that it does not exist
            if (checkExist == null)
            {
                return dal.insertRating(rating);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update RATING ;
        /// Update rating on condition that it exists
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool updateRating(Rating rating)
        {
            Rating checkExist = dal.findRating(rating);
            //Add rating on condition that it does not exist
            if (checkExist != null)
            {
                return dal.updateRating(rating);
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// GET LIST RATING WITH BOOK ID
        /// </summary>
        /// <param name="BookID"></param>
        /// <returns></returns>
        public List<Rating> getRatingWithBookID(string BookID)
        {
            return dal.getListRatingWithBookID(BookID);
        }

        /// <summary>
        /// GET LIST RATING WITH ACCOUNT ID
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public List<Rating> getRatingWithAccountID(string AccountID)
        {
            return dal.getListRatingWithAccount(AccountID);
        }

        public Rating findRating(Rating rating)
        {
            return dal.findRating(rating);
        }

        public List<User> getUserRating(String bookID, int rate)
        {
            return dal.getUserRating(bookID, rate);
        }
    }
}
