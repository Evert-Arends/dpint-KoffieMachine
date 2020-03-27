using KoffieMachineDomain;
using System.Collections.Generic;

namespace Dpint_wk456_KoffieMachine.Factories
{
    internal class DrinkFactory
    {
        public IEnumerable<string> DrinkNames => _drinks.Keys;

        private readonly Dictionary<string, IDrink> _drinks;

        public DrinkFactory()
        {
            _drinks = new Dictionary<string, IDrink>
            {
                ["Coffee"] = new Coffee(),
                ["Espresso"] = new Espresso(),
                ["Capuccino"] = new Capuccino(),
                ["Wiener Melange"] = new WienerMelange(),
                ["Café au Lait"] = new CafeAuLait(),
                ["Tea"] = new Tea()
            };
        }


        public IDrink GetDrink(string name)
        {
            return _drinks[name];
        } 
    }
}
