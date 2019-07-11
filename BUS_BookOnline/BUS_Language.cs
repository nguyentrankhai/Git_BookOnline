using DAL_BookOnline;
using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_BookOnline
{
    public class BUS_Language
    {
        DAL_Language dal = new DAL_Language();
        public List<Language> getAllLanguage()
        {
            return dal.getAllLanguage();
        }
        public bool insertLang(Language lang)
        {
            return dal.insertLang(lang);
        }
    }
}
