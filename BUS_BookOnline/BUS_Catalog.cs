using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_BookOnline;
using DTO_BookOnline;
namespace BUS_BookOnline
{
    public class BUS_Catalog
    {
        DAL_Catalog dal_cato = new DAL_Catalog();
        public List<Catalog> getAllCatalog()
        {
            return dal_cato.getAllCatalog();
        }
    }
}
