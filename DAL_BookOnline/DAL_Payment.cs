
using DTO_Book;
using System.Collections.Generic;
using System.Linq;

namespace DAL_BookOnline
{
    public class DAL_Payment
    {
        DataClassesBookOnlineDataContext db = null;

        private void createConnection()
        {
            db = new DataClassesBookOnlineDataContext();
        }

        public List<BranchPayment> getBranchPayment()
        {
            createConnection();
            List<BranchPayment> lstBranch = new List<BranchPayment>();
            var datas = db.tbl_BranchPayments.Select(x => x).ToList();
            foreach(tbl_BranchPayment t in datas)
            {
                lstBranch.Add(parseBranch(t));
            }
            return lstBranch;
        }
        private BranchPayment parseBranch(tbl_BranchPayment tbl)
        {
            BranchPayment payment = new BranchPayment(tbl.BranchCode, tbl.BranchName, tbl.Description, tbl.Status);
            return payment;
        }

        private tbl_BranchPayment parseBranch(BranchPayment tbl)
        {
            tbl_BranchPayment payment = new tbl_BranchPayment();
            payment.BranchCode = tbl.BranchCode;
            payment.BranchName = tbl.BranchName;
            payment.Description = tbl.Description;
            payment.Status = tbl.Status;
            return payment;
        }
    }
}
