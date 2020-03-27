using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain
{
    public class ChocolateAdapter : IDrink
    {
        protected readonly HotChocolate _adaptee;

        public ChocolateAdapter()
        {
            _adaptee = new HotChocolate();
        }
        public string Name
        {
            get => _adaptee.GetNameOfDrink();
            set { }
        }

        public double Price
        {
            get => _adaptee.Cost();
            set { }
        }

        public void LogStartDrink(ICollection<string> log)
        {
            log.Add($"Making {Name}...");
            foreach(string item in _adaptee.GetBuildSteps())
            {
                log.Add(item);
            }
        }
    }
}
