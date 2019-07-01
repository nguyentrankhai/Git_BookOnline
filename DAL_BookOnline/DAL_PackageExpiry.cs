using DTO_Book;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL_BookOnline
{
    public class DAL_PackageExpiry
    {
        DataClassesBookOnlineDataContext context;
        public List<PackageExpiry> getAllPackage()
        {
            context = new DataClassesBookOnlineDataContext();
            List<PackageExpiry> lst = new List<PackageExpiry>();
            var lstObj = context.tbl_Packages.ToList();
            foreach(tbl_Package p in lstObj)
            {
                PackageExpiry ex = new PackageExpiry();
                ex.PackageID = p.PackageID;
                ex.PackageName = p.PackageName;
                ex.PackagePrice = p.PackagePrice;
                ex.Note = p.Note;
                ex.Color = p.Color;
                ex.Amount = double.Parse(p.Amount.ToString());
                ex.PackageDay = p.PackageDay.Value;
                lst.Add(ex);
            }
            return lst;
        }
        public bool buyExpiry(string userid, string expiryid)
        {
            context = new DataClassesBookOnlineDataContext();
            bool flag = false;
            tbl_Account account = context.tbl_Accounts.Where(x => x.AccountID == userid).SingleOrDefault();
            tbl_Package package = context.tbl_Packages.Where(x => x.PackageID == expiryid).SingleOrDefault();
            if (account == null || package == null)
                return flag;

            if (account.Wallet > package.Amount)
            {
                account.Wallet -= package.Amount;
                DateTime dateAccount = (DateTime)account.RemainingTime;
                if (DateTime.Compare(dateAccount, DateTime.Today) < 0)
                {
                    account.RemainingTime = DateTime.Today.AddDays((double)package.PackageDay);
                }
                else
                {
                    account.RemainingTime = dateAccount.AddDays((double)package.PackageDay);
                }                            
                context.SubmitChanges();
                DAL_Transaction dal = new DAL_Transaction();
                dal.buyExpiry(userid, 0, "", "", expiryid);
                flag = true;
                
            }
            
            return flag;
        }
    }
}
