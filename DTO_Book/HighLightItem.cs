using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_BookOnline
{
    [Serializable]
    public class HighLightItem
    {
        private List<HighLightData> item;

        public List<HighLightData> Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }
        public HighLightItem()
        {
            item = new List<HighLightData>();
        }
    }
}
