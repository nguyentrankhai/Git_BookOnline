using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_BookOnline
{
    public class Follow
    {
        string accountID1, accountID2;
        int status;

        public string AccountID1
        {
            get
            {
                return accountID1;
            }

            set
            {
                accountID1 = value;
            }
        }

        public string AccountID2
        {
            get
            {
                return accountID2;
            }

            set
            {
                accountID2 = value;
            }
        }

        public int Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        public Follow ()
        { }
    }
}
