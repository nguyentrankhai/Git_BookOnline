using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_BookOnline;
namespace DAL_BookOnline
{
    public class DAL_Catalog
    {
        DataClassesBookOnlineDataContext db = new DataClassesBookOnlineDataContext();
        public List<Catalog> getAllCatalog()
        {
            List<Catalog> lstCatalog = new List<Catalog>();
            try
            {                
                var lst = (from catalo in db.tbl_Catalogs select catalo).ToList();
                if (lst != null)
                {
                    foreach (var cat in lst)
                    {
                        Catalog cata = new Catalog();
                        cata.Id = cat.CatalogID;
                        cata.Name = cat.CatalogNAME;
                        lstCatalog.Add(cata);
                    }
                }
            }
            catch
            {

            }
            return lstCatalog;
        }
    }
}
