using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class Capuccino : Drink
    {
        public Capuccino()
        {
            Name = "Capuccino";
            Price += 0.8;
        }

        public override void LogStartDrink(ICollection<string> log)
        {
            LogStartMaking(log);
            base.LogStartDrink(log);
            log.Add($"Finished making {Name}");
        }
    }
}
