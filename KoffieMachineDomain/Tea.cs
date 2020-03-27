using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class Tea : Drink, ISugar
    {
        public virtual bool HasSugar { get; set; }
        public virtual Amount SugarAmount { get; set; }
        public override string Name => "Tea";

        public override double GetPrice()
        {
            return BaseDrinkPrice;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting Tea bag....");
            log.Add("Filling with Water...");

            if (HasSugar)
            {
                log.Add($"Setting sugar amount to {SugarAmount}.");
                log.Add("Adding sugar...");
            }

            log.Add($"Finished making {Name}");
        }
    }
}
