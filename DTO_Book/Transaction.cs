using System;

namespace DTO_Book
{
    public class TransactionHistory
    {
        public string TransactionID { get; set; }
        public string TransactionName { get; set; }
        public string AccountID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string DiscountName { get; set; }
        public double Discount { get; set; }
        public double Amount { get; set; }        
    }
    public class TransactionInfo : TransactionHistory
    {
        public string TransactionID { get; set; }
        public string ProductID { get; set; }
        public new DateTime TransactionDate { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
