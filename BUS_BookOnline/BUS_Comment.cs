using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;
using DAL_BookOnline;

namespace BUS_BookOnline
{
    public class BUS_Comment
    {
        DAL_Comment dal_cmt = new DAL_Comment();

        public List<Comment> getAllComment()
        {
            return dal_cmt.getAllComment();
        }

        public List<Comment> getCommentsWithBookID(String bookID)
        {
            return dal_cmt.getCommentsWithBookID(bookID);
        }

        public List<Comment> getCommentsWithUserID(String userID)
        {
            return dal_cmt.getCommentsWithUserID(userID);
        }

        public bool updateComment(Comment comment)
        {
            return dal_cmt.updateComment(comment);
        }

        public bool insertComment(Comment comment)
        {
            return dal_cmt.insertComment(comment);
        }
    }
}
