using KoffieMachineDomain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using TeaAndChocoLibrary;

namespace Dpint_wk456_KoffieMachine.Factories
{
    internal class DrinkFactory
    {
        public IEnumerable<string> DrinkNames => _drinks.Keys;

        private readonly Dictionary<string, IDrink> _drinks;
        private readonly TeaBlend _defaultTeaBlend;
        public DrinkFactory()
        {
            var teaBlendFactory = new TeaBlendRepository();
            _defaultTeaBlend = teaBlendFactory.GetTeaBlend(teaBlendFactory.BlendNames.FirstOrDefault());
            _drinks = new Dictionary<string, IDrink>
            {
                ["Coffee"] = new StrengthDrinkDecorator(new Coffee(), strength: Strength.Normal),
                ["Espresso"] = new Espresso(),
                ["Capuccino"] = new Capuccino(),
                ["Wiener Melange"] = new WienerMelange(),
                ["Café au Lait"] = new CafeAuLait(),
                ["Tea"] = new TeaAdapter(_defaultTeaBlend)
            };
            //_drinks = MixDrink();
        }

        public IDrink MixDrink(string name, Amount? sugar = null, Amount? milk = null, Strength? strength = null, TeaBlend? blend = null, CustomCoffee coffee=null)
        {
            // GetValueOrDefault == If not null then use or else use given value
            // Can't seem to get around case? Ask Martijn.
            IDrink drink = null;

            switch (name)
            {
                case "Coffee":
                    drink = new StrengthDrinkDecorator(new Coffee(), strength.GetValueOrDefault(Strength.Normal));
                    break;

                case "Espresso":
                    drink = new StrengthDrinkDecorator(new Espresso(), Strength.Strong);
                    break;

                case "Capuccino":
                    drink = new StrengthDrinkDecorator(new Capuccino(), Strength.Normal);
                    break;

                case "Wiener Melange":
                    drink = new StrengthDrinkDecorator(new WienerMelange(), Strength.Weak);
                    break;

                case "Café au Lait":
                    drink = new CafeAuLait();
                    break;
                case "Tea":
                    drink = new TeaAdapter(blend.GetValueOrDefault(_defaultTeaBlend));
                    break;
                case "Chocolate":
                    drink = new ChocolateAdapter();
                    break;
                case "Chocolate Deluxe":
                    drink = new ChocolateDeluxeAdapter();
                    break;
                case "Custom":
                    drink = new CustomViewCoffee(coffee);
                    break;
            }

            if (sugar != null)
            {

                drink = new SugarDrinkDecorator(drink, sugar.GetValueOrDefault(Amount.Normal));
            }

            if (milk != null)
            {
                drink = new MilkDrinkDecorator(drink, milk.GetValueOrDefault(Amount.Normal));
            }

            return drink;
        }

        public IDrink GetDrink(string name)
        {
            return _drinks[name];
        } 
    }
}
