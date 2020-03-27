using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public abstract class Drink : IDrink
    {
        public string Name { get; set; }
        public double Price { get; set; }

        protected Drink()
        {
            Price = 1;
        }

        public virtual void LogDrinkMaking(ICollection<string> log)
        {

        }

        protected void LogStartMaking(ICollection<string> log)
        {
            log.Add($"Making {Name}...");
            log.Add($"Heating up...");
        }
    }
}
