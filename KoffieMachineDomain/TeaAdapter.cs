using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain
{
    public class TeaAdapter: IDrink
    {
        private readonly TeaAndChocoLibrary.Tea _adaptee;

        public TeaAdapter(TeaBlend blend)
        {
            _adaptee = new TeaAndChocoLibrary.Tea {Blend = blend};
            Name = "Tea";
            Price = TeaAndChocoLibrary.Tea.Price;
        }

        public string Name { get; set; }
        public double Price { get; set; }

        public void LogStartDrink(ICollection<string> log)
        {
            log.Add($"Making {Name}...");
            log.Add($"Heating up...");
            log.Add($"Adding blend {_adaptee.Blend.Name}");
            log.Add($"Finished making {Name}");
        }
    }
}
