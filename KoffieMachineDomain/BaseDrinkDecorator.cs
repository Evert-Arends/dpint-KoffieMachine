using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    class BaseDrinkDecorator : IDrink
    {
        public IDrink drink;

        public string Name
        {
            get => drink.Name;
            set => drink.Name = value;
        }

        public double Price
        {
            get => drink.Price;
            set => drink.Price = value;
        }

        public BaseDrinkDecorator(IDrink drink)
        {
            this.drink = drink;
        }

        public virtual void LogDrinkMaking(ICollection<string> log)
        {
            drink.LogDrinkMaking(log);
        }
    }
}
