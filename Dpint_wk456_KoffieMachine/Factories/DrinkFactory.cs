using KoffieMachineDomain;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

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
                ["Coffee"] = new StrengthDrinkDecorator(new Coffee(), strength: Strength.Normal),
                ["Espresso"] = new Espresso(),
                ["Capuccino"] = new Capuccino(),
                ["Wiener Melange"] = new WienerMelange(),
                ["Café au Lait"] = new CafeAuLait(),
                ["Tea"] = new Tea()
            };
            //_drinks = MixDrink();
        }

        public IDrink MixDrink(string name, Amount? sugar = null, Amount? milk = null, Strength? strength = null)
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

                //case "Chocolate":
                //    drink = new Chocolate();
                //    break;

                //case "Chocolate Deluxe":
                //    drink = new ChocolateDeluxe();
                //    break;

                //case "Tea":
                //    drink = new Tea(blend.GetValueOrDefault());
                //    break;

                //case "Irish Coffee":
                //    drink = new CreamDrinkDecorator(new IrishCoffee());
                //    break;

                //case "Italian Coffee":
                //    drink = new CreamDrinkDecorator(new ItalianCoffee());
                //    break;

                //case "Spanish Coffee":
                //    drink = new CreamDrinkDecorator(new SpanishCoffee());
                //    break;
            }

            if (sugar != null)
            {
                //if (name == "Tea")
                //{
                //    ((Tea)drink)?.SetSugar(sugar.GetValueOrDefault(Amount.Normal));
                //}
                //else
                {
                    drink = new SugarDrinkDecorator(drink, sugar.GetValueOrDefault(Amount.Normal));
                }
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
