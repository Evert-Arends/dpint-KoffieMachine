using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class BaseDrinkDecorator : IDrink
    {
        public IDrink Drink;

        public string Name
        {
            get => Drink.Name;
            set => Drink.Name = value;
        }

        public double Price
        {
            get => Drink.Price;
            set => Drink.Price = value;
        }

        public BaseDrinkDecorator(IDrink drink)
        {
            this.Drink = drink;
        }

        public virtual void LogStartDrink(ICollection<string> log)
        {
            Drink.LogStartDrink(log);
        }
    }
}
