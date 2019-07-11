using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Book
{
    public class Language
    {
        private string langID;
        private string langName;

        public string LangID
        {
            get
            {
                return langID;
            }

            set
            {
                langID = value;
            }
        }

        public string LangName
        {
            get
            {
                return langName;
            }

            set
            {
                langName = value;
            }
        }
        public Language()
        { }
    }
}
