using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class ChocolateDeluxeAdapter: ChocolateAdapter
    {
        public ChocolateDeluxeAdapter()
        {
            _adaptee.MakeDeluxe();
        }
    }
}
