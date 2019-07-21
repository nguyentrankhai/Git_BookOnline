using DAL_BookOnline;
using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS_BookOnline
{
    public class BUS_Publisher
    {
        DAL_Publisher dal = new DAL_Publisher();
        public List<Publisher> getAllPublisher()
        {
            return dal.getAllPublisher();
        }
    }
}
