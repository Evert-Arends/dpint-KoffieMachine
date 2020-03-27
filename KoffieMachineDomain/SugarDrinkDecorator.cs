using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    class SugarDrinkDecorator : BaseDrinkDecorator
    {

        public Amount Sugar { get; set; }
        public SugarDrinkDecorator(IDrink drink, Amount sugar) : base(drink)
        {
            Sugar = sugar;
            Price += 0.1; // Sugar
        }
        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting sugar amount to {Sugar}.");
            log.Add("Adding sugar...");
        }
    }
}
