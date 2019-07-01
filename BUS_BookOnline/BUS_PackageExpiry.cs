using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_BookOnline;
using DTO_Book;
namespace BUS_BookOnline
{
    public class BUS_PackageExpiry
    {
        public List<PackageExpiry> getAllPackageExpiry()
        {
            DAL_PackageExpiry dal = new DAL_PackageExpiry();
            return dal.getAllPackage();
        }
        public bool buyExpiry(string User_ID, string Expiry_ID)
        {
            DAL_PackageExpiry dal = new DAL_PackageExpiry();
            return dal.buyExpiry(User_ID, Expiry_ID);
        }
    }
}
