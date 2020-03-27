using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoffieMachineDomain;

namespace Dpint_wk456_KoffieMachine.Factories
{
    public class PaymentFactory
    {
        private readonly Dictionary<string, double> _cards;
        private readonly Dictionary<string, IPayment> _payments;

        public ICollection<string> Cards => _cards.Keys;
        public ICollection<string> Payments => _payments.Keys;

        public PaymentFactory()
        {
            _cards = new Dictionary<string, double>
            {
                ["Arjen"] = 5.0,
                ["Bert"] = 3.5,
                ["Chris"] = 7.0,
                ["Daan"] = 6.0
            };

            _payments = new Dictionary<string, IPayment>
            {
                ["Coin"] = new CashPayment(),
                ["Card"] = new PinPayment()
            };
        }

        public double GetCardValueByUsername(string name)
        {
            if (_cards.ContainsKey(name))
            {
                return _cards[name];
            }

            return _cards[Cards.First()];
        }

        public void SetAmountCard(string name, double amount)
        {
            if (_cards.ContainsKey(name))
            {
                _cards[name] = amount;
            }
        }

        public IPayment GetPaymentMethodByName(string name)
        {
            if (_payments.ContainsKey(name))
            {
                return _payments[name];
            }
            // Payments[0]
            return _payments[Payments.First()];
        }
    }
}
