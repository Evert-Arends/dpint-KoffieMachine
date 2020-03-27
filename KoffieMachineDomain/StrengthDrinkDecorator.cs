using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class StrengthDrinkDecorator : BaseDrinkDecorator
    {
    
        public Strength Strength { get; set; }

        public StrengthDrinkDecorator(IDrink drink, Strength strength) : base(drink)
        {
            Strength = strength;
        }

        public override void LogStartDrink(ICollection<string> log)
        {
            log.Add($"Setting strength to {Strength}.");
            base.LogStartDrink(log);
        }
        
    }
}
