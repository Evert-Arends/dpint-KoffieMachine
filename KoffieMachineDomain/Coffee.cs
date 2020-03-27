using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class Coffee : Drink, IStrength, ISugar, IMilk
    {
        private Strength _drinkStrength;

        public virtual bool HasSugar { get; set; }
        public virtual Amount SugarAmount { get; set; }
        public virtual bool HasMilk { get; set; }
        public virtual Amount MilkAmount { get; set; }

        public Strength DrinkStrength
        {
            get => _drinkStrength;
            set => _drinkStrength = value;
        }

        public override string Name => "Koffie";

        public override double GetPrice()
        {
            return BaseDrinkPrice;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with coffee...");

            if (HasSugar)
            {
                log.Add($"Setting sugar amount to {SugarAmount}.");
                log.Add("Adding sugar...");
            }

            if (HasMilk)
            {
                log.Add($"Setting milk amount to {MilkAmount}.");
                log.Add("Adding milk...");
            }

            log.Add($"Finished making {Name}");
        }
    }
}
