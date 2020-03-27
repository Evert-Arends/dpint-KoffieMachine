using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class CafeAuLait : Drink
    {
        public CafeAuLait()
        {
            Name = "CafeAuLait";
            Price += 0.5;
        }

        public override void LogStartDrink(ICollection<string> log)
        {
            LogStartMaking(log);
            base.LogStartDrink(log);
            log.Add("Filling half with coffee...");
            log.Add("Filling other half with milk...");
            log.Add($"Finished making {Name}");
        }
    }
}
