using System;
using System.Collections.Generic;
using System.Linq;
using DTO_BookOnline;

namespace DAL_BookOnline
{
    public class DAL_Rating
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();

        /// <summary>
        /// GET LIST RATING OF BOOK WITH BOOK ID
        /// </summary>
        /// <param name="bookID"></param>
        /// <returns></returns>
        public List<Rating> getListRatingWithBookID(string bookID)
        {
            List<Rating> lstRating = new List<Rating>();
            var ratings = db.tbl_Ratings.Where(x => x.BookID == bookID).ToList();
            if (ratings == null) return null;
            foreach (tbl_Rating rating in ratings)
            {
                Rating rate = new Rating(rating.AccountID, rating.BookID, rating.Rating);
                lstRating.Add(rate);
            }
            return lstRating;
        }

        /// <summary>
        /// GET LIST RATING OF BOOK WITH ACCOUNT
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public List<Rating> getListRatingWithAccount(string accountID)
        {
            List<Rating> lstRating = new List<Rating>();
            var ratings = db.tbl_Ratings.Where(x => x.BookID == accountID).ToList();
            if (ratings == null) return null;
            foreach (tbl_Rating rating in ratings)
            {
                Rating rate = new Rating(rating.AccountID, rating.BookID, rating.Rating);
                lstRating.Add(rate);
            }
            return lstRating;
        }

        /// <summary>
        /// Find Rating
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public Rating findRating(Rating rating)
        {
            var ratings = db.tbl_Ratings.Where((x => x.BookID == rating.BookID && x.AccountID == rating.AccountID)).SingleOrDefault();
            if (ratings == null)
            {
                return null;
            }
            Rating rate = new Rating(ratings.AccountID, ratings.BookID, ratings.Rating);
            return rate;
        }

        /// <summary>
        /// INSERT RATING
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool insertRating(Rating rating)
        {
            bool result = false;
            try
            {
                tbl_Rating rate = new tbl_Rating();
                rate.AccountID = rating.AccountID;
                rate.BookID = rating.BookID;
                rate.Rating = rating.Rate;
                db.tbl_Ratings.InsertOnSubmit(rate);
                db.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                //return (ex.Message);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// UPDATE RATING
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public bool updateRating(Rating rating)
        {
            try
            {
                tbl_Rating r = db.tbl_Ratings.Single(x => x.AccountID == rating.AccountID && x.BookID == rating.BookID);
                r.Rating = rating.Rate;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);

            }
            return false;
        }

        public List<User> getUserRating(String bookID, int rate)
        {
            List<User> lstUS = new List<User>();
            var ratings = db.tbl_Ratings.Where(x => x.BookID == bookID && x.Rating == rate).ToList();
            if (ratings == null) return null;
            foreach (tbl_Rating rating in ratings)
            {
                //Rating r = new Rating(rating.AccountID, rating.BookID, rating.Rating);
                DAL_User dalus = new DAL_User();
                lstUS.Add(dalus.getUser(rating.AccountID));
            }
            return lstUS;
        }
    }
}
