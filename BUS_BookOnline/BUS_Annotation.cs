
using System.Collections.Generic;
using DTO_Book;
using DAL_BookOnline;
namespace BUS_BookOnline
{
    public class BUS_Annotation
    {
        public List<Annotation> getAnnotationData(string userid, string bookid)
        {
            DAL_Annotation dal = new DAL_Annotation();
            return dal.getAnnotationData(userid, bookid);
        }
        public List<Annotation> getAnnotationData(string userid)
        {
            DAL_Annotation dal = new DAL_Annotation();
            return dal.getAnnotationData(userid);
        }
        public int insertAnnotationData(string userid, List<Annotation> lstAnno)
        {
            DAL_Annotation dal = new DAL_Annotation();
            return dal.insertAnnotationData(userid, lstAnno);
        }
        public int deleteAnnotationData(string userid, string bookid)
        {
            DAL_Annotation dal = new DAL_Annotation();
            return dal.deleteAnnotationData(userid, bookid);
        }
    }
}
