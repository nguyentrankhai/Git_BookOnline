using DTO_BookOnline;
using System.IO;
using System.Xml.Serialization;
using Template.Domain.Highlight;
using System;
using System.Xml;
using System.Xml.Schema;

namespace Template.Domain
{
    public class HighlightHandler
    {
        private string Path = ".\\xml\\";
        private object DeserializeFromXML(string FilePath)
        {
            FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            object obj = new object();
            try
            {
                XmlSerializer sr = new XmlSerializer(typeof(HighlightCollection));
                obj = sr.Deserialize(fs);
            }
            catch
            {
                obj = null;
            }
            fs.Close();
            return obj;
        }

        private void SerializeToXML(object data, string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            XmlSerializer sr = new XmlSerializer(typeof(HighlightCollection));

            sr.Serialize(fs, data);

            fs.Close();
        }

        public HighlightCollection open(User user)
        {
            isExistFilePath(Path);
            string filePath = Path + user.ID1.ToString() + ".xml";
            return (HighlightCollection)DeserializeFromXML(filePath);
        }       

        public void close(User user, object item)
        {
            isExistFilePath(Path);
            string filePath = Path + user.ID1.ToString() + ".xml";
            SerializeToXML(item, filePath);
        }
        private void isExistFilePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
