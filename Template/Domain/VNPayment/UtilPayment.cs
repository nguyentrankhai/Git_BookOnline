using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.VNPayment
{
    public class UtilPayment
    {
        public static bool isValidateAmountPayment(double amount)
        {
            if (amount < 10000)
                return false;
            bool temp = (amount % 2 != 0) && (amount % 10 != 0) && (amount % 5 != 0);
            if (temp)
            {
                return false;
            }
            return true;
        }
        private double validateAmount(double amount)
        {
            if (amount > 1000)
            {
                amount = amount / 10;
                return validateAmount(amount);
            }
            else
            {
                return amount = amount % 2;
            }
        }
    }
}
