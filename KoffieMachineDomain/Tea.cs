using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class Tea : Drink
    {
        public Tea()
        {
            Name = "Tea";
            Price += 0.5;

        }

        public override void LogStartDrink(ICollection<string> log)
        {
            log.Add($"Setting Tea bag....");
            log.Add("Filling with Water...");
            base.LogStartDrink(log);
            log.Add($"Finished making {Name}");
        }
    }
}
