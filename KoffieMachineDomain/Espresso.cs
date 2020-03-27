using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Espresso : Drink
    {
        public Espresso()
        {
            Name = "Espresso";
            Price += 0.7;
        }
        public override void LogStartDrink(ICollection<string> log)
        {
            LogStartMaking(log);
            base.LogStartDrink(log);
            log.Add($"Finished making {Name}");
        }
    }
}
