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
                        lstCatalog.Add(parseSQLtoDTO(cat));
                    }
                }
            }
            catch
            {

            }
            return lstCatalog;
        }
        private Catalog parseSQLtoDTO(tbl_Catalog cat)
        {
            Catalog cata = new Catalog();
            cata.Id = cat.CatalogID;
            cata.Name = cat.CatalogNAME;
            return cata;
        }

        public bool insertCatalog(Catalog cata)
        {
            bool result = false;
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Catalog us = new tbl_Catalog();
                us.CatalogID = cata.Id;
                us.CatalogNAME = cata.Name;
               
                context.tbl_Catalogs.InsertOnSubmit(us);
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
