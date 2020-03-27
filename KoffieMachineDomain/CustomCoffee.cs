using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KoffieMachineDomain
{
    public class CustomCoffee
    {

        public string Name { get; set; }
        public double Price { get; set; }
        public IList<string> steps { get; set; }

    }
}
