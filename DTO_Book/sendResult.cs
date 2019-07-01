using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_BookOnline
{
    public class sendResult
    {
        #region properties
        private String choose;
        private String str;

        public String Choose
        {
            get
            {
                return choose;
            }

            set
            {
                choose = value;
            }
        }

        public String Str
        {
            get
            {
                return str;
            }

            set
            {
                str = value;
            }
        }
        #endregion
        public sendResult()
        {

        }
        public sendResult(String choose, String str)
        {
            this.choose = choose;
            this.str = str;
        }
        public sendResult(String str)
        {
            this.str = str;
        }
    }
}
