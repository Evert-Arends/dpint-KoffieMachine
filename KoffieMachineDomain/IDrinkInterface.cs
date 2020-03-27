using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public interface IDrink
    {
        string Name { get; }
        double GetPrice();
        void LogDrinkMaking(ICollection<string> log);
    }
}