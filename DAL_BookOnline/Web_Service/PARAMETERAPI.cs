using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline.Web_Service
{
    class PARAMETERAPI
    {
        private string param;
        private string value;

        public PARAMETERAPI(string param, string value)
        {
            this.Param = param;
            this.Value = value;
        }

        public string Param
        {
            get
            {
                return param;
            }

            set
            {
                param = value;
            }
        }

        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }
}
