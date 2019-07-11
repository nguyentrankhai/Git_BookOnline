using DTO_Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline
{
    public class DAL_Language
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();
        private Language parseSQLtoDTO(tbl_Language lang)
        {
            Language language = new Language();
            if (lang != null)
            {
                language.LangID = lang.LangID;
                language.LangName = lang.LangName;
            }
            return language;
        }
        public Language getLanguage(string languageID)
        {
            if (languageID != "")
            {
                var lang = db.tbl_Languages.Where(x => x.LangID == languageID).SingleOrDefault();

                return parseSQLtoDTO(lang);
            }
            return null;
        }

    }
}
