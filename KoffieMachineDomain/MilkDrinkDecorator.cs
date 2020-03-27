using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class MilkDrinkDecorator : BaseDrinkDecorator
    {

        public Amount Milk { get; set; }
        public MilkDrinkDecorator(IDrink drink, Amount milk) : base(drink)
        {
            Milk = milk;
            Price += 0.15;
        }
        public override void LogStartDrink(ICollection<string> log)
        {
            base.LogStartDrink(log);
            log.Add($"Setting milk amount to {Milk}.");
            log.Add("Adding milk...");
        }
    }
}
