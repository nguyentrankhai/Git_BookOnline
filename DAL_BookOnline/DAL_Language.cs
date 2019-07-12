using DTO_BookOnline;
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
        public List<Language> getAllLanguage()
        {
            var langs = db.tbl_Languages.ToList();
            if (langs == null) return null;

            List<Language> lstlang = new List<Language>();
            foreach (tbl_Language lang in langs)
            {

                lstlang.Add(parseSQLtoDTO(lang));
            }
            return lstlang;
        }

        public bool insertLang(Language lang)
        {
            bool result = false;
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Language us = new tbl_Language();
                us.LangID = lang.LangID;
                us.LangName = lang.LangName;

                context.tbl_Languages.InsertOnSubmit(us);
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
