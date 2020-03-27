using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CustomViewCoffee: Drink
    {
        private CustomCoffee _customCoffee;
        public CustomViewCoffee(CustomCoffee customCoffee)
        {
            _customCoffee = customCoffee;
            Price += customCoffee.Price;
            Name = customCoffee.Name;

        }
        public override void LogStartDrink(ICollection<string> log)
        {
            LogStartMaking(log);
            foreach (var step in _customCoffee.steps)
            {
                log.Add(step);
            }
            log.Add($"Finished making {Name}");

        }
    }
}
