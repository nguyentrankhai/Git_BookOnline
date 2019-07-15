using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;
using DAL_BookOnline;

namespace DAL_BookOnline
{
    public class DAL_Comment
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();

        private Comment baseSQLtoDTO(tbl_Comment comment)
        {
            DAL_User dal_user = new DAL_User();
            if (comment != null)
            {

                Comment cmt = new Comment();
                cmt.BookID = comment.BookID;
                cmt.UserID = comment.AccountID;
                cmt.Note = comment.Note;
                cmt.Date = comment._Date;
                cmt.Status = comment._Status;
                cmt.Content = comment.Comment;

                DAL_User dalUs = new DAL_User();
                User us = dalUs.getUser(cmt.UserID);
                if (us != null)
                {
                    cmt.Image = us.Image1;
                }
                else
                { cmt.Image = null; }

                DAL_Rating dal = new DAL_Rating();
                Rating rate = dal.findRating(new Rating(cmt.UserID, cmt.BookID, 0));
                if (rate == null)
                {
                    cmt.Rate = 0;
                }
                else if (rate.Rate != null)
                {
                    cmt.Rate = rate.Rate;
                }
                DAL_Book dalBK = new DAL_Book();
                if (dalBK.findBook(cmt.UserID, cmt.BookID))
                {
                    cmt.Note = "Đã mua sách";
                }
                else
                {
                    cmt.Note = "";
                }
                return cmt;
            }
            return null;
        }

        public List<Comment> getAllComment()
        {
            var comments = db.tbl_Comments.ToList();
            if (comments == null) return null;

            List<Comment> lstComment = new List<Comment>();
            foreach (tbl_Comment cmt in comments)
            {
                if (cmt._Status == 1) { 
                lstComment.Add(baseSQLtoDTO(cmt));
                }
            }
            return lstComment;
        }

        public List<Comment> getCommentsWithBookID(String bookID)
        {
            var comments = db.tbl_Comments.Where(x => x.BookID == bookID);
            if (comments == null) return null;

            List<Comment> lstComment = new List<Comment>();
            foreach (tbl_Comment cmt in comments)
            {

                if (cmt._Status == 1)
                {
                    lstComment.Add(baseSQLtoDTO(cmt));
                }
            }
            return lstComment;
        }
        private Comment parseLinQtoDTO(tbl_Comment tblComment)
        {
            if (tblComment != null)
            {
                Comment cmt = new Comment();
                cmt.BookID = tblComment.BookID;
                cmt.UserID = tblComment.AccountID;
                cmt.Note = tblComment.Note;
                cmt.Date = tblComment._Date;
                cmt.Status = tblComment._Status;
                cmt.Content = tblComment.Comment;

                DAL_User dalUs = new DAL_User();
                User us = dalUs.getUser(cmt.UserID);
                if (us != null)
                {
                    cmt.Image = us.Image1;
                }
                else cmt.Image = null;
                return cmt;
            }
            return null;
        }

        public List<Comment> getCommentsWithUserID(String userID)
        {
            var comments = db.tbl_Comments.Where(x => x.AccountID == userID);
            if (comments == null) return null;

            List<Comment> lstComment = new List<Comment>();
            foreach (tbl_Comment cmt in comments)
            {
                if (cmt._Status == 1) { 
                lstComment.Add(baseSQLtoDTO(cmt));
                }
            }
            return lstComment;
        }

        public bool updateComment(Comment comment)
        {
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Comment cmt = context.tbl_Comments.Single(x => x._Date == comment.Date && x.BookID == comment.BookID && x.AccountID == comment.UserID);
                cmt.Comment = comment.Content;

                db.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool insertComment(Comment comment)
        {
            bool result = false;
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Comment cmt = new tbl_Comment();
                cmt.AccountID = comment.UserID;
                cmt.BookID = comment.BookID;
                cmt._Date = DateTime.Now;
                cmt._Status = 1;
                cmt.Comment = comment.Content;
                context.tbl_Comments.InsertOnSubmit(cmt);
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
