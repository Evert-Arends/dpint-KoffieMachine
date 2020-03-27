using KoffieMachineDomain;

namespace Dpint_wk456_KoffieMachine.Factories
{
    internal class AdditionFactory
    {

        /*
         * Check type, add extra's if type is correct, else return with no extra additions.
         */
        public IDrink AddMilk(IDrink drink, Amount amount)
        {
            if (!(drink is IMilk milkDrink)) return drink;
            milkDrink.MilkAmount = amount;
            milkDrink.HasMilk = true;

            return (IDrink) milkDrink;
        }

        public IDrink AddSugar(IDrink drink, Amount amount)
        {
            if (!(drink is ISugar sugarDrink)) return drink;
            sugarDrink.HasSugar = true;
            sugarDrink.SugarAmount = amount;

            return (IDrink) sugarDrink;
        }

        public IDrink AddStrength(IDrink drink, Strength strength)
        {
            if (!(drink is IStrength strengthDrink)) return drink;
            strengthDrink.DrinkStrength = strength;

            return (IDrink) strengthDrink;
        }

        public IDrink AddAll(IDrink drink, Strength strength, Amount milkAmount, Amount sugarAmount)
        {
            drink = this.AddStrength(
                this.AddMilk(
                    this.AddSugar(
                        drink, sugarAmount
                        ),
                    milkAmount), 
                strength);
            return drink;
        }

    }
}
