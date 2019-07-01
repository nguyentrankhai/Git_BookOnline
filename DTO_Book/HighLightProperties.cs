using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DTO_BookOnline
{
    public class HighLightProperties
    {
        /// <summary>
        /// Đọc file XML
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        ///
        #region Method
        public object DeserializeFromXML(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);                       
            object obj = new object();
            try
            {
                XmlSerializer sr = new XmlSerializer(typeof(HighLightItem));
                obj = sr.Deserialize(fs);
            }
            catch
            {
                obj = null;
            }
            fs.Close();
            return obj;
        }

        /// <summary>
        /// Lưu file XML
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filePath"></param>
        public void SerializeToXML(object data, string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            XmlSerializer sr = new XmlSerializer(typeof(HighLightItem));

            sr.Serialize(fs, data);
            
            fs.Close();
        }
        /// <summary>
        /// Mở file XML và convert sang kiểu HighlightItem.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        public HighLightItem open(User user)
        {
            string filePath = ".\\xml\\" + user.ID1.ToString() + ".xml";
            return (HighLightItem)DeserializeFromXML(filePath);
        }

        public void close(User user, object item)
        {
            string filePath = ".\\xml\\" + user.ID1.ToString() + ".xml";
            this.SerializeToXML(item, filePath);
        }
        #endregion
    }
}
