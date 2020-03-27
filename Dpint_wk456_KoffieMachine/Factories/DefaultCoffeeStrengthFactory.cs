using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KoffieMachineDomain;

namespace Dpint_wk456_KoffieMachine.Factories
{
    class DefaultCoffeeStrengthFactory
    {
        public IEnumerable<string> CoffeeEnumerable => _drinks.Keys;
        private readonly Dictionary<string, Strength> _drinks;

        public DefaultCoffeeStrengthFactory()
        {
            _drinks = new Dictionary<string, Strength>
            {
                ["Espresso"] = Strength.Strong,
                ["Capuccino"] = Strength.Normal,
                ["Wiener Melange"] = Strength.Weak
            };
        }

        public Strength GetStrength(string name)
        {
            if(CoffeeEnumerable.Contains(name))
            {
                return _drinks[name];
            }
            else
            {
                throw new KeyNotFoundException();
            }
        }
    }
}
