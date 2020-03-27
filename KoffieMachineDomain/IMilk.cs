using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public interface IMilk
    {
        bool HasMilk { get; set; }
        Amount MilkAmount { get; set; }

    }
}
