using System;
using System.Collections.Generic;
using System.Linq;
using DTO_Book;
using System.Data;
using System.Data.SqlClient;

namespace DAL_BookOnline
{
    public class DAL_Transaction
    {
        DataClassesBookOnlineDataContext context;
        public bool buyBooks(string AccountID, double Discount, string DiscountName, string Note, List<tbl_Book> lstBook)
        {
            bool b = false;
            try
            {
                context = new DataClassesBookOnlineDataContext();
                DateTime now = DateTime.Now;
                string transID = now.ToString("yyyyMMddhhmmss");
                double Amount = 0;
                tbl_TransactionInfo info;
                foreach (tbl_Book item in lstBook)
                {
                    Amount += (double)item.PRICE;
                    info = new tbl_TransactionInfo();
                    info.TransactionID = transID;
                    info.TransactionDate = now;
                    info.ProductID = item.BookID;
                    info.ProductName = item.BookNAME;
                    info.Quantity = lstBook.Where(x => x.BookID == item.BookID).ToList().Count();
                    info.Price = item.PRICE;
                    if (context.tbl_TransactionInfos.Where(x => x.TransactionID == info.TransactionID && x.ProductID == info.ProductID).ToList().Count() > 0)
                        continue;
                    context.tbl_TransactionInfos.InsertOnSubmit(info);
                    context.SubmitChanges();
                }
                Amount -= Discount;

                tbl_TransactionHistory transHistory = new tbl_TransactionHistory();
                transHistory.TransactionID = transID;
                transHistory.AccountID = AccountID;
                transHistory.TransactionDate = now;
                transHistory.TransactionName = "Mua sach";
                transHistory.Amount = (decimal)Amount;
                transHistory.Discount = (decimal)Discount;
                transHistory.DiscountName = DiscountName;
                transHistory.Note = Note;

                context.tbl_TransactionHistories.InsertOnSubmit(transHistory);
                context.SubmitChanges();
                b = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                b = false;
            }

            return b;
        }
        public List<tbl_TransactionHistory> getLichSuMuaHang(string AccountID)
        {
            List<tbl_TransactionHistory> lst = new List<tbl_TransactionHistory>();
            context = new DataClassesBookOnlineDataContext();
            lst = context.tbl_TransactionHistories.Where(x => x.AccountID == AccountID).ToList();
            return lst;
        }
        public List<tbl_TransactionInfo> getLichSuInfo(string TransactionID)
        {
            List<tbl_TransactionInfo> lst = new List<tbl_TransactionInfo>();
            context = new DataClassesBookOnlineDataContext();
            lst = context.tbl_TransactionInfos.Where(x => x.TransactionID == TransactionID).ToList();
            return lst;
        }
        public bool buyExpiry(string AccountID, double Discount, string DiscountName, string Note, string PackageID)
        {
            bool b = false;
            context = new DataClassesBookOnlineDataContext();
            DateTime now = DateTime.Now;
            string transID =AccountID + now.ToString("yyyyMMddhhmmss");
            tbl_TransactionInfo info = new tbl_TransactionInfo();
            tbl_Package package = new tbl_Package();
            package = context.tbl_Packages.Where(x => x.PackageID == PackageID).SingleOrDefault();
            info.TransactionID = transID;
            info.TransactionDate = now;
            info.Quantity = 1;
            info.Price = package.Amount;
            info.ProductName = package.PackageName;
            info.ProductID = package.PackageID;
            tbl_TransactionHistory history = new tbl_TransactionHistory();
            history.TransactionID = transID;
            history.AccountID = AccountID;
            history.TransactionDate = now;
            history.TransactionName = "Mua goi thoi han";
            history.Note = Note;
            history.Amount = package.Amount - (decimal)Discount;
            history.DiscountName = DiscountName;
            history.Discount = (decimal)Discount;
            try
            {
                context.tbl_TransactionInfos.InsertOnSubmit(info);
                context.tbl_TransactionHistories.InsertOnSubmit(history);
                context.SubmitChanges();
                b=true;
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
                b = false;
            }
            return b;
        }
        public List<TransactionInfo> getReport(string transID)
        {
            context = new DataClassesBookOnlineDataContext();
            //context.Connection.ConnectionString
            //string strSQL = (from x in context.tbl_DanhMucs where x.ID == "QUERY_TRANSACTION_INFO" select x.SQL).SingleOrDefault();
            //DataSet lst = context.ExecuteQuery<object>(strSQL.Replace("&Trans", transID)).ToList();
            List<TransactionInfo> Infos = new List<TransactionInfo>();
            //foreach(var info in lst)
            //{
            //    Infos.Add(new TransactionInfo()
            //    {
            //        TransactionID = info,

            //    });
            //}
            //return Infos;
            try
            {
                string strSQL = (from x in context.tbl_DanhMucs where x.ID == "QUERY_TRANSACTION_INFO" select x.SQL).SingleOrDefault();
                SqlConnection con = new SqlConnection(context.Connection.ConnectionString);
                con.Open();
                SqlCommand command = new SqlCommand(strSQL.Replace("&Trans", transID), con);
                SqlDataReader ds = command.ExecuteReader();
                while (ds.Read())
                {
                    Infos.Add(new TransactionInfo()
                    {
                        TransactionID = ds.GetValue(0).ToString(),
                        TransactionName = ds.GetValue(1).ToString(),
                        TransactionDate = DateTime.Parse(ds.GetValue(2).ToString()),
                        AccountID = ds.GetValue(3).ToString(),
                        ProductID = ds.GetValue(5).ToString(),
                        ProductName = ds.GetValue(6).ToString(),
                        Quantity = int.Parse(ds.GetValue(7).ToString()),
                        Price = double.Parse(ds.GetValue(8).ToString()),
                        Amount = double.Parse(ds.GetValue(9).ToString()),
                        DiscountName = ds.GetValue(10).ToString(),
                        Discount = double.Parse(ds.GetValue(11).ToString())
                    });
                }

                con.Close();
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Infos;
        }
        public bool RechargeWallet(string userID, double amount)
        {
            context = new DataClassesBookOnlineDataContext();
            tbl_TransactionInfo info = new tbl_TransactionInfo();
            tbl_TransactionHistory his = new tbl_TransactionHistory();
            his.AccountID = userID;
            his.Amount = (decimal)amount;
            his.Discount = 0;
            his.DiscountName = "";
            his.Note = "Nap vi tai khoan";
            his.TransactionDate = DateTime.Now; 
            his.TransactionID = userID + DateTime.Now.ToString("hhmmss");
            his.TransactionName= "Nap vi tai khoan";

            info.Price = (decimal)amount;
            info.ProductID = "NAPTIEN";
            info.ProductName = "NAP VI TAI KHOAN";
            info.Quantity = 1;
            info.TransactionDate = DateTime.Now; ;
            info.TransactionID = his.TransactionID;
            try
            {
                context.tbl_TransactionInfos.InsertOnSubmit(info);
                context.tbl_TransactionHistories.InsertOnSubmit(his);
                

                tbl_Account account = new tbl_Account();
                account = context.tbl_Accounts.Where(x => x.AccountID == userID).SingleOrDefault();
                account.Wallet += (decimal)amount;

                context.SubmitChanges();
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
