using DTO_Book;
using System.Collections.Generic;
using System.Linq;
using XMindAPI.LIB;

namespace Template.Domain.SaveFile
{
    public class SaveXmindFile
    {
        public void SaveXmind(List<Annotation> lst, string fileName)
        {
            XMindWorkBook workbook = new XMindWorkBook(fileName, false);
            IEnumerable<string> sheetName = lst.Select(x => x.Book_name).Distinct();
            List<string> lstBookName = sheetName.ToList();
            List<DataSheet> lstSheet = new List<DataSheet>();
            foreach (string s in lstBookName)
            {
                lstSheet.Add(new DataSheet() { SheetID = workbook.AddSheet(s), SheetName = s });
            }
            List<DataSheet> lstTopicID = new List<DataSheet>();
            foreach (DataSheet s in lstSheet)
            {
                string topicID = workbook.AddCentralTopic(s.SheetID, s.SheetName, XMindStructure.Map);
                lstTopicID.Add(new DataSheet() { SheetID = topicID, SheetName = s.SheetName });
            }
            foreach (Annotation ann in lst)
            {
                string sheetID = lstTopicID.Where(x => x.SheetName == ann.Book_name).SingleOrDefault().SheetID;
                string topicid = workbook.AddTopic(sheetID, ann.Text);
                if (ann.Comment != null)
                {
                    workbook.AddTopic(topicid, ann.Comment);
                }
            }
            workbook.Save();
        }
    }
}
