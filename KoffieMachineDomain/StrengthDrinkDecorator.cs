using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    class StrengthDrinkDecorator : BaseDrinkDecorator
    {
    
        public Strength Strength { get; set; }

        public StrengthDrinkDecorator(IDrink drink, Strength strength) : base(drink)
        {
            Strength = strength;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting strength to {Strength}.");
        }
        
    }
}
