namespace DTO_Book
{
    public class BranchPayment
    {
        private string branchCode;
        private string branchName;
        private string description;
        private string status;

        public BranchPayment(string branchCode, string branchName, string description, string status)
        {
            this.branchCode = branchCode;
            this.branchName = branchName;
            this.description = description;
            this.status = status;
        }

        public string BranchCode
        {
            get
            {
                return branchCode;
            }

            set
            {
                branchCode = value;
            }
        }

        public string BranchName
        {
            get
            {
                return branchName;
            }

            set
            {
                branchName = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public string Status
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
    }
}
