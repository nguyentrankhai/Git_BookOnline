namespace DTO_BookOnline
{
   public class Rating
    {
        private string accountID;
        private string bookID;
        private int rate;

        public string AccountID
        {
            get
            {
                return accountID;
            }

            set
            {
                accountID = value;
            }
        }

        public string BookID
        {
            get
            {
                return bookID;
            }

            set
            {
                bookID = value;
            }
        }

        public int Rate
        {
            get
            {
                return rate;
            }

            set
            {
                rate = value;
            }
        }
        public Rating()
        {
        }
        public Rating(string accountID, string bookID, int rate)
        {
            this.accountID = accountID;
            this.bookID = bookID;
            this.rate = rate;
        }
    }
}
