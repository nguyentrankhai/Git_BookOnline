using System;
using DTO_Book;
using DevExpress.Pdf;
using System.Windows.Media;
using System.Linq;
using System.Collections.Generic;

namespace DAL_BookOnline
{
    public class DAL_Annotation
    {
        DataClassesBookOnlineDataContext db = null;

        private void createConnection()
        {
            db = new DataClassesBookOnlineDataContext();
        }

        public int deleteAnnotationData(string userid, string bookid)
        {
            createConnection();
            List<tbl_Annotation> lst = db.tbl_Annotations.Where(x => x.book_id == bookid && x.user_id == userid).ToList();
            db.tbl_Annotations.DeleteAllOnSubmit(lst);
            db.SubmitChanges();
            return lst.Count;
        }

        public int insertAnnotationData(string userid, List<Annotation> lstAnno)
        {
            createConnection();
            List<tbl_Annotation> lstTbl = new List<tbl_Annotation>();
            lstTbl = loopAnnotation(lstAnno, true);

            db.tbl_Annotations.InsertAllOnSubmit(lstTbl);
            db.SubmitChanges();
            return lstAnno.Count;
        }

        public List<Annotation> getAnnotationData(string userid, string bookid)
        {
            List<Annotation> obj = new List<Annotation>();
            createConnection();
            List<tbl_Annotation> data = db.tbl_Annotations.Where(a => a.user_id == userid && a.book_id == bookid).ToList();
            obj = loopAnnotation(data);
            return obj;
        }

        public List<Annotation> getAnnotationData(string userid)
        {
            List<Annotation> obj = new List<Annotation>();
            createConnection();
            List<tbl_Annotation> data = db.tbl_Annotations.Where(a => a.user_id == userid).ToList();
            obj = loopAnnotation(data);
            return obj;
        }

        private List<Annotation> loopAnnotation(List<tbl_Annotation> lst)
        {
            List<Annotation> objAnno = new List<Annotation>();
            foreach(tbl_Annotation item in lst)
            {
                objAnno.Add(parseAnnotation(item));
            }
            return objAnno;
        }
        private Annotation parseAnnotation(tbl_Annotation obj)
        {
            double[] quart = new double[8];
            quart[0] = (double) obj.QX1;
            quart[1] = (double) obj.qy1;
            quart[2] = (double) obj.qx2;
            quart[3] = (double) obj.qy2;
            quart[4] = (double) obj.qx3;
            quart[5] = (double) obj.qy3;
            quart[6] = (double) obj.qx4;
            quart[7] = (double) obj.qy4;
            Color c = (Color)ColorConverter.ConvertFromString(obj.Annotation_color);
            Annotation anno = new Annotation(
                obj.Annotation_id,
                obj.user_id,
                obj.user_name,
                obj.book_name,
                obj.book_id,
                (int)obj.page_number,
                obj.Annotation_text,
                (int)obj.page_number,
                parseAnnotationType(obj.Annotation_type),
                c,
                (DateTime)obj.creationDate,
                (DateTime)obj.modificationDate,
                obj.Subject,
                quart,
                obj.Annotation_Comment
                );
            return anno;
        }

        private PdfTextMarkupAnnotationType parseAnnotationType(string strType)
        {
            PdfTextMarkupAnnotationType type = new PdfTextMarkupAnnotationType();
            Enum.TryParse(strType, true, out type);
            return type;
        }

        private tbl_Annotation parseAnnotation(Annotation anno)
        {
            tbl_Annotation tbl = new tbl_Annotation();
            tbl.Annotation_id = anno.Annotation_id;
            tbl.Annotation_color = anno.Annotation_color.ToString();
            tbl.user_id = anno.Iduser;
            tbl.user_name = anno.User_name;
            tbl.Annotation_text = anno.Text;
            tbl.Annotation_type = anno.Annotation_type.ToString();
            tbl.creationDate = anno.CreationDate;
            tbl.modificationDate = anno.ModificationDate;
            tbl.page_number = anno.Page;
            tbl.Subject = anno.Subject;
            tbl.Annotation_Comment = anno.Comment;
            tbl.QX1 = (decimal)anno.Quart[0];
            tbl.qy1 = (decimal)anno.Quart[1];
            tbl.qx2 = (decimal)anno.Quart[2];
            tbl.qy2 = (decimal)anno.Quart[3];
            tbl.qx3 = (decimal)anno.Quart[4];
            tbl.qy3 = (decimal)anno.Quart[5];
            tbl.qx4 = (decimal)anno.Quart[6];
            tbl.qy4 = (decimal)anno.Quart[7];
            return tbl;
        }

        private tbl_Annotation parseAnnotationNoKey(Annotation anno)
        {
            tbl_Annotation tbl = new tbl_Annotation();
            tbl.Annotation_color = anno.Annotation_color.ToString();
            tbl.user_id = anno.Iduser;
            tbl.user_name = anno.User_name;
            tbl.book_id = anno.Idbook;
            tbl.book_name = anno.Book_name;
            tbl.Annotation_text = anno.Text;
            tbl.Annotation_type = anno.Annotation_type.ToString();
            tbl.creationDate = anno.CreationDate;
            tbl.modificationDate = anno.ModificationDate;
            tbl.page_number = anno.Page;
            tbl.Subject = anno.Subject;
            tbl.Annotation_Comment = anno.Comment;
            tbl.QX1 = (decimal)anno.Quart[0];
            tbl.qy1 = (decimal)anno.Quart[1];
            tbl.qx2 = (decimal)anno.Quart[2];
            tbl.qy2 = (decimal)anno.Quart[3];
            tbl.qx3 = (decimal)anno.Quart[4];
            tbl.qy3 = (decimal)anno.Quart[5];
            tbl.qx4 = (decimal)anno.Quart[6];
            tbl.qy4 = (decimal)anno.Quart[7];
            return tbl;
        }

        private List<tbl_Annotation> loopAnnotation(List<Annotation> lst, bool isInsert)
        {
            List<tbl_Annotation> objAnno = new List<tbl_Annotation>();
            if (isInsert)
            {
                foreach (Annotation item in lst)
                {
                    objAnno.Add(parseAnnotationNoKey(item));
                }
            }
            else
            {
                foreach (Annotation item in lst)
                {
                    objAnno.Add(parseAnnotation(item));
                }
            }
            
            return objAnno;
        }
    }
}
