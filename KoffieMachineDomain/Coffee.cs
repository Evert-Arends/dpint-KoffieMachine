using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class Coffee : Drink
    {
        public Coffee()
        {
            Name = "Coffee";
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            LogStartMaking(log);
            log.Add($"Finished making {Name}");
        }
    }
}
