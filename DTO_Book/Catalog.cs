using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_BookOnline
{
    public class Catalog
    {
        #region properties
        private string name;
        private string id;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        #endregion

        #region Initalize
        public Catalog()
        {

        }
        public Catalog(string name)
        {
            this.name = name;
        }
        public Catalog(string id, string name)
        {
            this.id = id;
            this.name = name;
        }
        #endregion

    }
}
