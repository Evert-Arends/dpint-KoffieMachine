using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class PinPayment : IPayment
    {
        public double Pay(double amount, double paid)
        {
            if (amount <= paid)
            {
                return 0; // Pay what you can, fill up with coins later. (From the original source).
            }
            else
            {
                return amount - paid;
            }
        }
    }
}