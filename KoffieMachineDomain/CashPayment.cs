using System;

namespace KoffieMachineDomain
{
    public class CashPayment : IPayment
    {
        public double Pay(double amount, double paid)
        {
            return Math.Max(Math.Round(amount - paid, 2), 0);
        }
    }
}
