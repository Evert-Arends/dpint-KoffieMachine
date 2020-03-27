using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public interface IDrink
    {
        string Name { get; set; }
        double Price { get; set; }

        void LogStartDrink(ICollection<string> log);
    }
}