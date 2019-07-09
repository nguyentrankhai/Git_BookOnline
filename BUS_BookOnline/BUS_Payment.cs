using DAL_BookOnline;
using DTO_Book;
using System.Collections.Generic;

namespace BUS_BookOnline
{
    public class BUS_Payment
    {
        public List<BranchPayment> getBranchPayment()
        {
            DAL_Payment dal = new DAL_Payment();
            return dal.getBranchPayment();
        }
        public bool isRechargeWallet(string userID, double amount)
        {
            DAL_Transaction dal = new DAL_Transaction();
            return dal.RechargeWallet(userID, amount);
        }
    }
}
