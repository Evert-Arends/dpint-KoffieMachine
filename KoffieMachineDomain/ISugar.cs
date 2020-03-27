using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public interface ISugar
    {
        bool HasSugar { get; set; }
        Amount SugarAmount { get; set; }
    }
}
